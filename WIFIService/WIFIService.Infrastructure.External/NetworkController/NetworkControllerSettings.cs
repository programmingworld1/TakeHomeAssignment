namespace WIFIService.Infrastructure.External.NetworkController;

public class NetworkControllerSettings
{
    public const string SectionName = "ExternalServices:NetworkControllerApi";

    public string NetworkControllerBaseUrl { get; init; } = string.Empty;
}
