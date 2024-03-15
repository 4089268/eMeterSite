using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models;
using eMeterSite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
    
    [Auth]
    [Route("[controller]")]
    public class MeasurementController : Controller
    {
        private readonly IAppService appService;
        private readonly ILogger<MeasurementController> _logger;

        public MeasurementController(ILogger<MeasurementController> logger, IAppService appService)
        {
            _logger = logger;
            this.appService = appService;
        }

        public async Task<IActionResult> Index( [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta, [FromQuery] string? deviceAddress = null  ) 
        {
            ViewData["Title"] = "Measurement";
            
            MeasurementViewModel measurementViewModel = new();
            if( desde!= null && hasta != null){
                measurementViewModel.Desde = desde.Value;
                measurementViewModel.Hasta = hasta.Value;
            }

            // Get devices list
            var enumerableResponse = await this.appService.GetMeasurement(0, 0, measurementViewModel.Desde, measurementViewModel.Hasta, deviceAddress);
            if( enumerableResponse == null)
            {
                // TODO: Redirect to bad request
                return RedirectToAction("Errro", "Home");
            }

            // Prepare data to view
            IEnumerable<Measurement>? measurements = enumerableResponse.Data;
            ViewData["TotalItems"] = enumerableResponse.TotalItems;
            ViewData["Measurements"] = measurements??[];

            
            return View( measurementViewModel );
        }


    }
}   