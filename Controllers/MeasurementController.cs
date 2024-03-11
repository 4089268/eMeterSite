using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
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

        public async Task<IActionResult> Index([FromQuery] int page = 0, [FromQuery] int chunk = 25) 
        {

            // Get devices list
            var enumerableResponse = await this.appService.GetDevices(chunk, page);
            if( enumerableResponse == null)
            {
                // TODO: Redirect to bad request
                return RedirectToAction("Errro", "Home");
            }

            IEnumerable<DeviceInfo>? devices = enumerableResponse.Data;

            ViewData["ChunkSize"] = enumerableResponse.ChunkSize;
            ViewData["CurrentPage"] = enumerableResponse.Page;
            ViewData["TotalItems"] = enumerableResponse.TotalItems;
            ViewData["Devices"] = devices!;

            return View();
        }


    }
}   