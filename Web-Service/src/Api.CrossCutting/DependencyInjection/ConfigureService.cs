using Api.CrossCutting.Communication.Interfaces;
using Api.CrossCutting.Communication.Sender;
using Api.CrossCutting.Communication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            // Communication services
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<IEmailService, EmailService>();
        }
    }
}