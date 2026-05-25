using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WIFIService.Infrastructure.External;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
