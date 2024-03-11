using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eMeterSite.Views.Shared
{
    public class SideBar : PageModel
    {
        private readonly ILogger<SideBar> _logger;

        public SideBar(ILogger<SideBar> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}