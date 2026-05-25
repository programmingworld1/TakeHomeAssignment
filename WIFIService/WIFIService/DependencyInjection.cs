using WIFIService.Api.Middlewares;

namespace WIFIService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<BusinessProblemDetailsFactory>();

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
