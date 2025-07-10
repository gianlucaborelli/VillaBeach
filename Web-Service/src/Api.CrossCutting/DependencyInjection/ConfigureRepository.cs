using Api.Core.Events;
using Api.Core.Mediator;
using Api.CrossCutting.Bus;
using Api.Data.Context;
using Api.Data.EventSourcing;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Data.Repository.EventSourcing;
using Api.Domain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MyContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped<IMediatorHandler, InMemoryBus>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();            
            
            builder.Services.AddScoped(typeof(IEventStoreRepository), typeof(EventStoreSqlRepository));
            builder.Services.AddScoped<IEventStore, SqlEventStore>();
            builder.Services.AddDbContext<EventStoreSqlContext>(
                    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}