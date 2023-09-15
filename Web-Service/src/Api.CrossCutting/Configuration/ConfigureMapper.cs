
using Api.CrossCutting.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.Configuration
{
    public static class ConfigureMapper
    {
        public static void ConfigureMapperService(this IServiceCollection serviceCollection)
        {
            var mapperConfiguration = new AutoMapper.MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new DtoToModelProfile());
                configuration.AddProfile(new EntityToDtoProfile());
                configuration.AddProfile(new EntityToModelProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            serviceCollection.AddSingleton(mapper);
        }
    }
}