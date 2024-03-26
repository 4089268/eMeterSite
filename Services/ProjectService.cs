using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using eMeterSite.Models;
using eMeterSite.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

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

        public async Task<bool?> CreateProject( NewProjectViewModel newProject ){
            
            var httpContext = httpContextAccessor.HttpContext ?? throw new Exception("Cant access to the httpContext");
            var token = httpContextAccessor.HttpContext!.Session.GetString("JWTToken") ?? throw new Exception("The user token is not defined");
            
            var httpClient =  httpClientFactory.CreateClient("eMeterApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync("api/projects", newProject);

            if( !response.IsSuccessStatusCode )
            {
                if( response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity ){
                    throw new ValidationException( "The calve is already stored" );   
                }

                logger.LogError("Can get the projects at ProjectService.GetProjects; {statusCode}", response.StatusCode);
                return null;
            }

            return true;
        }

    }
}