using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using eMeterSite.Models;

namespace eMeterSite.Services
{
    public class ProjectService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProjectService> logger;

        public ProjectService( IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, ILogger<ProjectService> logger){
            this.httpContextAccessor = httpContextAccessor;
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;            
        }

        public async Task<IEnumerable<Project>?> GetProjects(){
            
            var httpContext = httpContextAccessor.HttpContext ?? throw new Exception("Cant access to the httpContext");
            var token = httpContextAccessor.HttpContext!.Session.GetString("JWTToken") ?? throw new Exception("The user token is not defined");
            
            var httpClient =  httpClientFactory.CreateClient("eMeterApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("api/projects");

            if( !response.IsSuccessStatusCode )
            {
                logger.LogError("Can get the projects at ProjectService.GetProjects; {statusCode}", response.StatusCode);
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<IEnumerable<Project>>();
            return data;
        }

    }
}