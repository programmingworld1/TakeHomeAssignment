namespace WIFIService.Infrastructure.External.NetworkController;

public class NetworkControllerSettings
{
    public const string SectionName = "ExternalServices:NetworkControllerApi";

    public string BaseUrl { get; init; } = string.Empty;
}
