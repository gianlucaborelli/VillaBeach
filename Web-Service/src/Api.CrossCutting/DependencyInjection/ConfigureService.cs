using Api.Domain.Interfaces.Services.Login;
using Api.Domain.Interfaces.Services.Product;
using Api.Domain.Interfaces.Services.ProductPrice;
using Api.Domain.Interfaces.Services.Purchase;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IProductPriceService, ProductPriceService>();
            serviceCollection.AddTransient<IPurchaseService, PurchaseService>();
            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}