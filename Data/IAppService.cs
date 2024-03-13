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

        public Task<EnumerableResponse<Measurement>?> GetMeasurement( int chunk=0, int page=0, DateTime? from = null, DateTime? to = null );

        public Task<IEnumerable<Project>?> GetProjects();

    }
}