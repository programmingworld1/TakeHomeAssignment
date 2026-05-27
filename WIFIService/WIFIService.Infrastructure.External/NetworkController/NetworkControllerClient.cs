using System.Net.Http.Json;
using System.Text.Json;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Infrastructure.External.NetworkController.Models;

namespace WIFIService.Infrastructure.External.NetworkController;

public class NetworkControllerClient : INetworkControllerClient
{
    private static readonly JsonSerializerOptions CamelCase = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ILogger<NetworkControllerClient> _logger;

    public NetworkControllerClient(
        HttpClient httpClient,
        IOptions<NetworkControllerSettings> settings,
        IMapper mapper,
        ILogger<NetworkControllerClient> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);
        _mapper = mapper;
        _logger = logger;
    }

    public async Task ActivateAsync(NetworkActivationRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending activation request to Network Controller for customer {CustomerId}", 
            request.CustomerId);

        var payload = _mapper.Map<NetworkActivationPayload>(request);

        var response = await _httpClient.PostAsJsonAsync(
            "activate",
            payload,
            CamelCase,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Network Controller returned {(int)response.StatusCode}");
        }
    }
}
