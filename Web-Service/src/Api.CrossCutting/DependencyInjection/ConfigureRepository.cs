using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Repository.EventSourcing;
using Api.Core.Mediator;
using Api.CrossCutting.Bus;
using Api.Data.EventSourcing;
using Api.Core.Events;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this IServiceCollection services)
        {        
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<MyContext>(
                    options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
            );
                       
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped(typeof(IEventStoreRepository), typeof(EventStoreSqlRepository));
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddDbContext<EventStoreSqlContext>(
                    options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
            );
        }
    }
}