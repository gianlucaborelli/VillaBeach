using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.Configuration
{
    public static class ConfigureMapper
    {
        public static void ConfigureMapperService(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var mapperConfigExpression = new MapperConfigurationExpression();

            mapperConfigExpression.AddProfile<DtoToCommand>();
            mapperConfigExpression.AddProfile<EntityToDtoProfile>();

            var mapperConfiguration = new MapperConfiguration(mapperConfigExpression, loggerFactory);

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}