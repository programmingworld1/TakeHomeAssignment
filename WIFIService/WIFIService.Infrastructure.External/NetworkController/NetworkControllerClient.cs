using System.Net.Http.Json;
using System.Text.Json;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Infrastructure.External.NetworkController.Models;

namespace WIFIService.Infrastructure.External.NetworkController;

public class NetworkControllerClient : INetworkControllerClient
{
    private static readonly JsonSerializerOptions CamelCase = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private readonly HttpClient _httpClient;
    private readonly ILogger<NetworkControllerClient> _logger;

    public NetworkControllerClient(HttpClient httpClient, IOptions<NetworkControllerSettings> settings, ILogger<NetworkControllerClient> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);
        _logger = logger;
    }

    public async Task ActivateAsync(NetworkActivationRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending activation request to Network Controller for customer {CustomerId}", request.CustomerId);

        var payload = request.Adapt<NetworkActivationPayload>();

        var response = await _httpClient.PostAsJsonAsync("activate", payload, CamelCase, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Network Controller returned {StatusCode} for customer {CustomerId}",
                (int)response.StatusCode, request.CustomerId);
        }

        response.EnsureSuccessStatusCode();
    }
}
