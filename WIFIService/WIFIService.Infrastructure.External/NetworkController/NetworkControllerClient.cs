using WIFIService.Application.Clients;

namespace WIFIService.Infrastructure.External.NetworkController;

public class NetworkControllerClient : INetworkControllerClient
{
    private readonly HttpClient _httpClient;

    public NetworkControllerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task ActivateAsync(NetworkActivationRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
