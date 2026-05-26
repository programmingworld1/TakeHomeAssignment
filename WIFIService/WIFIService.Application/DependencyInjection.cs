using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WIFIService.Application.WifiProvisioning.EnableWifi;

namespace WIFIService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEnableWifiService, EnableWifiService>();

        return services;
    }
}
