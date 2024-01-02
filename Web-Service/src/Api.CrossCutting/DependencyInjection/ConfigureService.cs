using Api.Domain.Interfaces.Services.Authentication;
using Api.Domain.Interfaces.Services.Product;
using Api.Domain.Interfaces.Services.ProductPrice;
using Api.Domain.Interfaces.Services.Purchase;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Api.Service.Security;
using Microsoft.Extensions.DependencyInjection;
using Api.Domain.Interfaces.Services.Email;
using Api.Service.Helpers;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IUserSettingsService, UserSettingsService>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IProductPriceService, ProductPriceService>();
            serviceCollection.AddTransient<IPurchaseService, PurchaseService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<IRefreshTokenService, RefreshTokenService>();
            serviceCollection.AddTransient<IAccessTokenService, AccessTokenService>();
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            serviceCollection.AddTransient<IEmailService, EmailService>();
        }
    }
}