using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this WebApplicationBuilder builder)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var mapperConfigExpression = new MapperConfigurationExpression();

            mapperConfigExpression.AddProfile<DtoToCommand>();
            mapperConfigExpression.AddProfile<EntityToDtoProfile>();

            var mapperConfiguration = new MapperConfiguration(mapperConfigExpression, loggerFactory);

            var mapper = mapperConfiguration.CreateMapper();

            builder.Services.AddSingleton(mapper);
        }
    }
}