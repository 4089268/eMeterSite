using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using eMeterSite.Data;
using eMeterSite.Models;
using eMeterSite.Models.ViewModels;
using eMeterSite.Services;

namespace eMeterSite.Controllers
{
    
    [Auth]
    [Route("[controller]")]
    public class ProjectsController(ILogger<ProjectsController> logger, ProjectService projectService) : Controller
    {
        private readonly ILogger<ProjectsController> _logger = logger;
        private readonly ProjectService projectService = projectService;

    
        public async Task<IActionResult> Index()
        {
            var projects = Array.Empty<Project>();
            try
            {
                var response =  await projectService.GetProjects();
                if( response != null){
                    projects = response.ToArray();
                }
            }
            catch (System.Exception ex)
            {
                //TODO: Handle the error
                ViewData["ErrorMessage"] = ex.Message;
            }

            return View( projects );

        }
        
    }
}