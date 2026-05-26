namespace WIFIService.Infrastructure.External.NetworkInfrastructure;

public class NetworkInfrastructureSettings
{
    public const string SectionName = "ExternalServices:NetworkInfrastructureApi";

    public string BaseUrl { get; init; } = string.Empty;
}
