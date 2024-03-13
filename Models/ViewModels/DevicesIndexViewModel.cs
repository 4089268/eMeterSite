using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMeterSite.Models.ViewModels
{
    public class DevicesIndexViewModel
    {

        public IEnumerable<DeviceInfo> Devices {get;set;} = null!;

        public string ValveStatus {get;set;} = "";
        public string BatteryStatus {get;set;} = "";


    }
}