using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using eMeterSite.Data;
using eMeterSite.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace eMeterSite.Services
{
    public class AppService : IAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<AppService> logger;

        public AppService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, ILogger<AppService> logger){
            _httpContextAccessor = httpContextAccessor;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<EnumerableResponse<DeviceInfo>?> GetDevices( int chunk=25, int page=0)
        {
            // * Retrive the token form the session
            var userToken =  this._httpContextAccessor.HttpContext!.Session.GetString("JWTToken");
            if( string.IsNullOrEmpty( userToken)){
                return null;
            }

            // * Create the client
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // * Make thre request
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
            // * Retrive the token form the session
            var userToken =  this._httpContextAccessor.HttpContext!.Session.GetString("JWTToken");
            if( string.IsNullOrEmpty( userToken)){
                return null;
            }

            // * Creat the client
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken );  

            // * Make the request
            var httpResponse = httpClient.GetAsync($"/api/Measurement/Devices/{deviceAddress}");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cat get devices data" );
                return null;
            }

            // * Prepare the response
            var devicesDetails = await httpResponse.Result.Content.ReadFromJsonAsync<DeviceDetails>();
            return devicesDetails;
        }

        public async Task<EnumerableResponse<Measurement>?> GetMeasurement(int chunk = 0, int page = 0, DateTime? from = null, DateTime? to = null, string? deviceAddress = null)
        {
            // * Preparing the parameters for the request
            var queryParamsList = new List<string>();
            queryParamsList.Add($"chunk={chunk}");
            queryParamsList.Add($"page={page}");
            if(from != null && to != null){
                queryParamsList.Add($"from={from.Value.ToString("yyy-MM-dd")}");
                queryParamsList.Add($"to={to.Value.ToString("yyy-MM-dd")}");
            }
            if(deviceAddress != null){
                queryParamsList.Add($"deviceAddress={deviceAddress}");
            }
            var _queryParams = string.Join( "&", queryParamsList);

            // * Retrive the token form the session
            var userToken =  this._httpContextAccessor.HttpContext!.Session.GetString("JWTToken");
            if( string.IsNullOrEmpty( userToken)){
                return null;
            }

            // * Creat the client
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");

            // * Make the request
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken ); 
            var httpResponse = httpClient.GetAsync($"/api/Measurement?{_queryParams}");
            if( httpResponse.IsFaulted ){
                this.logger.LogError( httpResponse.Exception, "Cat get measurement data" );
                return null;
            }

            // * Process the response
            var measurementData = await httpResponse.Result.Content.ReadFromJsonAsync<EnumerableResponse<Measurement>>();
            if(measurementData == null){
                return null;
            }
            return measurementData;
        }

        public async Task<IEnumerable<Project>?> GetProjects()
        {
            var userToken =  this._httpContextAccessor.HttpContext!.Session.GetString("JWTToken");
            if( string.IsNullOrEmpty( userToken)){
                return null;
            }
            
            var httpClient = this.httpClientFactory.CreateClient("eMeterApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var httpResponse = httpClient.GetAsync($"/api/Projects");
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