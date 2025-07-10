using Api.CrossCutting.Identity.Authentication;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.CrossCutting.Identity.Data.Context;
using Api.CrossCutting.Identity.JWT.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.CrossCutting.Identity.Extensions
{
    public static class AuthenticationConfiguration
    {
        public static WebApplicationBuilder AddAuthenticationConfiguration(this WebApplicationBuilder builder)
        {
            builder.ConfigureAuthentication();
            builder.ConfigureIdentity();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ILoggedInUser, LoggedInUser>();

            builder.Services.AddTransient<IJwtAuthManager, JwtAuthManager>();            

            builder.Services.AddDbContext<IdentityContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));                 

            return builder;
        }

        private static void ConfigureIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var jwtSection = builder.Configuration.GetSection("JwtSettings");
            var secret = jwtSection["Secret"] ?? throw new ApplicationException("Token key is not configured.");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}