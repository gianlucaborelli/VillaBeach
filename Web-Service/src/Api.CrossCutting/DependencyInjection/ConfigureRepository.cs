using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();            
            serviceCollection.AddScoped<IProductRepository, ProductImplementation>();
            serviceCollection.AddScoped<IProductPriceRepository, ProductPriceImplementation>();
            serviceCollection.AddScoped<IPurchaseRepository, PurchaseImplementation>();

            serviceCollection.AddDbContext<MyContext>(
            options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
            );
        }
    }
}