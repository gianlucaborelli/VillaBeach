
using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Configuration
{
    public static class ConfigureMapper
    {
        public static void ConfigureMapperService(this IServiceCollection serviceCollection)
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new DtoToCommand());
                configuration.AddProfile(new EntityToDtoProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            serviceCollection.AddSingleton(mapper);
        }
    }
}