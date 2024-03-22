using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models.ViewModels;
using eMeterSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
    
    [Auth]
    [Route("[controller]")]
    public class ProjectsController(ILogger<ProjectsController> logger, IAppService appService) : Controller
    {
        private readonly ILogger<ProjectsController> _logger = logger;
        private readonly  IAppService appService = appService;

    
        public IActionResult Index()
        {
            return View();
        }
        

    }
}