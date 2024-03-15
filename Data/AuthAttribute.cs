using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMeterSite.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eMeterSite.Data
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        private readonly JwtSettings jwtSettings;

        public AuthAttribute()
        {
            this.jwtSettings = eMeterSite.Helpers
                .StaticSettings
                .GetConfiguration()
                .GetSection("JwtSettings")
                .Get<JwtSettings>()!;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context != null){
                
                // Validate session
                var token = context.HttpContext.Session.GetString("JWTToken");
                if(token == null){
                    context.HttpContext.Response.Redirect("/authentication");
                    return;
                }

                // Validate token
                try {
                    context.HttpContext.User = TokenValidator.ValidateToken( token, jwtSettings );
                }
                catch (Exception ) {
                    context.HttpContext.Response.Redirect("/authentication");
                    return;
                }
            }
        }
    }
}