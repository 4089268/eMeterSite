using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace eMeterSite.Services
{
    public class AppService : IAppService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<AppService> logger;

        public AppService( IHttpClientFactory httpClientFactory, ILogger<AppService> logger){
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<EnumerableResponse<DeviceInfo>?> GetDevices( int chunk=25, int page=0)
        {
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");

            var httpResponse = httpClient.GetAsync($"/api/Measurement/Devices?chunk={chunk}&page={page}");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cat get devices data" );
                return null;
            }

            var devicesList = await httpResponse.Result.Content.ReadFromJsonAsync<EnumerableResponse<DeviceInfo>>();
            if(devicesList == null){
                return null;
            }
            return devicesList;
        }

        public async Task<DeviceDetails?> GetDeviceInfo(string deviceAddress)
        {
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");

            var httpResponse = httpClient.GetAsync($"/api/Measurement/Devices/{deviceAddress}");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cat get devices data" );
                return null;
            }

            var devicesDetails = await httpResponse.Result.Content.ReadFromJsonAsync<DeviceDetails>();
            return devicesDetails;
        }

        public async Task<EnumerableResponse<Measurement>?> GetMeasurement(int chunk = 0, int page = 0, DateTime? from = null, DateTime? to = null)
        {
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");

            var queryParamsList = new List<string>();
            queryParamsList.Add($"chunk={chunk}");
            queryParamsList.Add($"page={page}");

            if( from != null && to != null){
                queryParamsList.Add($"from={from.Value.ToString("yyy-MM-dd")}");
                queryParamsList.Add($"to={to.Value.ToString("yyy-MM-dd")}");
            }
            var _queryParams = string.Join( "&", queryParamsList);
            var httpResponse = httpClient.GetAsync($"/api/Measurement?{_queryParams}");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cat get measurement data" );
                return null;
            }

            var measurementData = await httpResponse.Result.Content.ReadFromJsonAsync<EnumerableResponse<Measurement>>();
            if(measurementData == null){
                return null;
            }
            return measurementData;
        }

        public async Task<IEnumerable<Project>?> GetProjects()
        {
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");

            var httpResponse = httpClient.GetAsync($"/api/Project");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cant get projects" );
                return null;
            }

            var projects = await httpResponse.Result.Content.ReadFromJsonAsync<IEnumerable<Project>>();
            if(projects == null){
                return null;
            }
            return projects;
        }
    }
}