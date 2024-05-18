using Api.Service.Interfaces;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Api.CrossCutting.Identity.Authentication;
using Api.CrossCutting.Communication.Interfaces;
using Api.CrossCutting.Communication.Sender;
using Api.CrossCutting.Communication.Services;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IUserSettingsService, UserSettingsService>();
            serviceCollection.AddTransient<IUserAddressService, UserAddressService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IPurchaseService, PurchaseService>();
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<IEmailService, EmailService>();
        }
    }
}