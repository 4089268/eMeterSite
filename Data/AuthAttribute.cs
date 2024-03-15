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
        private readonly JwtSettings _jwtSettings;

        public AuthAttribute()
        {
            // TODO: Inject this settings
            _jwtSettings =  new JwtSettings(){
                Issuer="https://emeter.arquos.ddns.net",
                Audience="https://emeter.arquos.ddns.net",
                Key="9ef445d23b9443d78ef6c48864c8ec43"
            };
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
                    context.HttpContext.User = TokenValidator.ValidateToken( token, _jwtSettings );
                }
                catch (Exception ) {
                    context.HttpContext.Response.Redirect("/authentication");
                    return;
                }
            }
        }
    }
}