using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.CrossCutting.Configuration
{
    public static class ConfigureAuth
    {
        public static void ConfigureAuthentication(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenKey = Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY") 
                        ?? throw new ApplicationException("Token key is not configured.");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}