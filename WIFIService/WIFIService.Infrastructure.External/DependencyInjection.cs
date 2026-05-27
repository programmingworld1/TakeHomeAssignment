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
        services.Configure<SpeedProfilesClientSettings>(
            configuration.GetSection(SpeedProfilesClientSettings.SectionName));

        services.Configure<NetworkControllerSettings>(
            configuration.GetSection(NetworkControllerSettings.SectionName));

        services.ConfigureHttpClientDefaults(builder =>
        {
            builder.AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3; // retry up to 3 times
                // wait 1 second between retries
                // Combined with the default Exponential backoff type, the actual delays grow with each attempt: ~1s, ~2s, ~4s — so later
                // retries wait progressively longer, giving the downstream service more time to recover
                options.Retry.Delay = TimeSpan.FromSeconds(1);
            });
        });

        services.AddHttpClient<ISpeedProfilesClient, SpeedProfilesClient>();
        services.AddHttpClient<INetworkControllerClient, NetworkControllerClient>();

        return services;
    }
}
