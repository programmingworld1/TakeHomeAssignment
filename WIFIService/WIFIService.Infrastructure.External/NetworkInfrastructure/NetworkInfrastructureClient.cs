using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WIFIService.Application.Clients.NetworkInfrastructure;
using WIFIService.Infrastructure.External.NetworkInfrastructure.Models;

namespace WIFIService.Infrastructure.External.NetworkInfrastructure;

public class NetworkInfrastructureClient : INetworkInfrastructureClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NetworkInfrastructureClient> _logger;

    public NetworkInfrastructureClient(
        HttpClient httpClient, 
        IOptions<NetworkInfrastructureSettings> settings, 
        ILogger<NetworkInfrastructureClient> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);
        _logger = logger;
    }

    public async Task<IEnumerable<SpeedProfile>> GetSpeedProfilesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching speed profiles from Network Infrastructure API");

        var httpResponse = await _httpClient.GetAsync("speed-profiles", 
            cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Network Infrastructure API returned {(int)httpResponse.StatusCode}");
        }

        var response = await httpResponse
            .Content.ReadFromJsonAsync<NetworkInfrastructureResponse>(cancellationToken)
            ?? throw new HttpRequestException("Network Infrastructure API returned an empty response.");

        var profiles = response.SpeedProfiles
            .Select(p => new SpeedProfile(p.Code, p.DownloadSpeedMbps, p.UploadSpeedMbps))
            .ToList();

        _logger.LogInformation("Retrieved {Count} speed profiles from Network Infrastructure API", 
            profiles.Count);

        return profiles;
    }
}
