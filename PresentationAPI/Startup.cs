using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;

[assembly: OwinStartup(typeof(PresentationAPI.Startup))]

namespace PresentationAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            string jwtSecret = "A_VERY_LONG_AND_SUPER_SECRET_KEY_FOR_RECIPE_API_32_CHARS_OR_MORE";

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }
                });
        }
    }
}