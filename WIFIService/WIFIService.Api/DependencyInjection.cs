using FluentValidation;
using WIFIService.Api.Mappings;
using WIFIService.Api.Middlewares.Errors;

namespace WIFIService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<BusinessProblemDetailsFactory>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMappings();
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
