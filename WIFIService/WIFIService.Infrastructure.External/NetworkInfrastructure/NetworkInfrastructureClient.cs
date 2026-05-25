using System.Net.Http.Json;
using WIFIService.Application.Clients;

namespace WIFIService.Infrastructure.External.NetworkInfrastructure;

public class NetworkInfrastructureClient : INetworkInfrastructureClient
{
    private readonly HttpClient _httpClient;

    public NetworkInfrastructureClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<SpeedProfile>> GetSpeedProfilesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<NetworkInfrastructureResponse>(
            "speed-profiles", cancellationToken);

        return response!.SpeedProfiles
            .Select(p => new SpeedProfile(p.Code, p.DownloadSpeedMbps, p.UploadSpeedMbps));
    }
}
