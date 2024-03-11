using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Models;

namespace eMeterSite.Data
{
    public interface IAppService
    {
        public Task<EnumerableResponse<DeviceInfo>?> GetDevices(int chunk=25, int page=0);
        public Task<DeviceDetails?> GetDeviceInfo(string deviceAddress);

    }
}