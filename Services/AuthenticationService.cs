using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using eMeterSite.Data.Contracts;
using eMeterSite.Models;

namespace eMeterSite.Services
{
    public class AuthenticationService
    {
        
        private readonly IHttpClientFactory httpClientFactory;

        public AuthenticationService( IHttpClientFactory factory){
            this.httpClientFactory = factory;
        }

        public string? Authenticate( IUserCredentials credentials, out string message ){
            
            var httpClient = httpClientFactory.CreateClient("eMeterApi");
            var httpResponse = httpClient.PostAsJsonAsync("/api/Authentication", new {
                email = credentials.Email,
                password = credentials.Password
            }).Result;

            if( !httpResponse.IsSuccessStatusCode ){
                if( httpResponse.StatusCode == HttpStatusCode.BadRequest){
                    message = "Usuario y/o contraseña invalidos";
                    return null;
                }
                message = "Error al tratar de validar las credenciales; " + httpResponse.StatusCode;
                return null;
            }

            var authenticationResponse = httpResponse.Content.ReadFromJsonAsync<AuthenticationResponse>().GetAwaiter().GetResult();

            if( authenticationResponse != null){
                message = "";
                return authenticationResponse.Token;
            }else{
                message = "Fail to parse content data";
                return null;
            }
        }
    }
}