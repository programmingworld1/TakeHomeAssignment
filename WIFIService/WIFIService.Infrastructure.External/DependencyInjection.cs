using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIFIService.Application.Clients;
using WIFIService.Infrastructure.External.NetworkController;
using WIFIService.Infrastructure.External.NetworkInfrastructure;

namespace WIFIService.Infrastructure.External;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<INetworkInfrastructureClient, NetworkInfrastructureClient>();
        services.AddHttpClient<INetworkControllerClient, NetworkControllerClient>();

        return services;
    }
}
