using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Models.ViewModels;
using eMeterSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationService authService) : Controller
    {
        private readonly ILogger<AuthenticationController> _logger = logger;
        private readonly AuthenticationService authenticationService = authService;

    
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login( AuthenticationViewModel authenticationViewModel)
        {

            var token = authenticationService.Authenticate( authenticationViewModel, out string? message );

            if( token == null){
                authenticationViewModel.MessageError = message;
                ViewData["ErrorMessage"] = "Usuario y/o contraseña incorrectos.";
                return View("index", authenticationViewModel);
            }
            
            // Store token securely, e.g., in session
            HttpContext.Session.SetString( "JWTToken", token);

            return RedirectToAction("Index", "Home");
        }

    }
}