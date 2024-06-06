using Api.CrossCutting.Identity.Authentication;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.CrossCutting.Identity.Data.Context;
using Api.CrossCutting.Identity.JWT.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Identity.Extensions
{
    public static class AddAuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationUserConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoggedInUser, LoggedInUser>();

            services.AddTransient<IJwtAuthManager, JwtAuthManager>();

            services.AddDbContext<IdentityContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL")));

            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
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

            return services;
        }
    }
}