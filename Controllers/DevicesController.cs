using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Controllers
{
    
    [Auth]
    [Route("[controller]")]
    public class DevicesController : Controller
    {
        private readonly IAppService appService;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ILogger<DevicesController> logger, IAppService appService)
        {
            _logger = logger;
            this.appService = appService;
        }

        public async Task<IActionResult> Index([FromQuery] int page = 0, [FromQuery] int chunk = 25, [FromQuery] int PI = 0, [FromQuery] string? SB = null, [FromQuery] string? SV = null ) 
        {

            // Get devices list
            var enumerableResponse = await this.appService.GetDevices(chunk, page);
            if( enumerableResponse == null)
            {
                // TODO: Redirect to bad request
                return RedirectToAction("", "Home");
            }

            // Process data
            IEnumerable<DeviceInfo>? devices = enumerableResponse.Data;
            ViewData["ChunkSize"] = enumerableResponse.ChunkSize;
            ViewData["CurrentPage"] = enumerableResponse.Page;
            ViewData["TotalItems"] = enumerableResponse.TotalItems;
            ViewData["Devices"] = devices!;

            // Get catalogs projects
            var _projects = await appService.GetProjects();
            if( _projects != null){
                ViewData["Projects"] = _projects!;
            }

            // ViewData["Filter"] = new DeviceFilterViewModel(){
            //     ProjectId = 2
            // };

            return View();
        }

        [HttpGet]
        [Route("Show/{deviceAddress}")]
        public async Task<IActionResult> Show([FromRoute] string deviceAddress)
        {

            var deviceDetails = await this.appService.GetDeviceInfo( deviceAddress );
            
            if( deviceDetails == null)
            {
                // TODO: Redirect to bad request
                return RedirectToAction("", "Home");
            }

            ViewData["Device"] = deviceDetails!;

            return View();
        }
        
    }

    public class DeviceFilterViewModel {
        public string? StatusValve {get;set;}
        public string? StatusBattery {get;set;}
        public int ProjectId {get;set;}
    }
}   