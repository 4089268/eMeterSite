using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController(ILogger<AuthenticationController> logger) : Controller
    {
        private readonly ILogger<AuthenticationController> _logger = logger;

    
        public IActionResult Index()
        {
            return View();
        }

    }
}