using Mapster;
using MapsterMapper;

namespace WIFIService.Api.Mappings
{
    public static class MapperDependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
