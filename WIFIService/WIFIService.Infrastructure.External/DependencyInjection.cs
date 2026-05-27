using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Application.Clients.NetworkInfrastructure;
using WIFIService.Infrastructure.External.NetworkController;
using WIFIService.Infrastructure.External.NetworkInfrastructure;

namespace WIFIService.Infrastructure.External;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<NetworkInfrastructureSettings>(
            configuration.GetSection(NetworkInfrastructureSettings.SectionName));

        services.Configure<NetworkControllerSettings>(
            configuration.GetSection(NetworkControllerSettings.SectionName));

        services.ConfigureHttpClientDefaults(builder =>
        {
            builder.AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.Delay = TimeSpan.FromSeconds(1);
            });
        });

        services.AddHttpClient<INetworkInfrastructureClient, NetworkInfrastructureClient>();
        services.AddHttpClient<INetworkControllerClient, NetworkControllerClient>();

        return services;
    }
}
