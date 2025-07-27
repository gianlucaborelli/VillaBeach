using Api.CrossCutting.Communication.Settings;
using Api.CrossCutting.Configuration;
using Api.CrossCutting.Identity.Extensions;
using Api.CrossCutting.Identity.JWT.Settings;
using CrossCutting.Configuration;
using CrossCutting.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CrossCutting.Extensions
{
    public static class ExtensionsConfiguration
    {
        public static void AddExtensions(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));            
            
            builder.AddAutoMapperConfiguration();
            builder.AddRepositoryDependencies();
            builder.AddServicesDependencies();
            builder.AddCommandHandlers();
            builder.AddAuthenticationConfiguration();
            builder.AddSwaggerConfiguration();

            builder.Services.AddRazorPages();
            builder.Services.AddLogging();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            builder.Services.AddScoped<ModelBindingFailureFilter>();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        }

        public static void AddUsings(this WebApplication app)
        {
            app.UseSwaggerConfiguration();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseStaticFiles();

            app.UseProblemDetailsExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();
        }        
    }
}
