using Api.CrossCutting.Communication.Interfaces;
using Api.CrossCutting.Communication.Sender;
using Api.CrossCutting.Communication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Configuration
{
    public static class ServicesConfigurations
    {
        public static void AddServicesDependencies(this WebApplicationBuilder builder)
        {
            // Communication services
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddTransient<IEmailService, EmailService>();
        }
    }
}