namespace WIFIService.Infrastructure.External.NetworkInfrastructure;

public class SpeedProfilesClientSettings
{
    public const string SectionName = "ExternalServices:NetworkInfrastructureApi";

    public string BaseUrl { get; init; } = string.Empty;
}
