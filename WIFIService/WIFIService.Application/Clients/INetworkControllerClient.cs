namespace WIFIService.Application.Clients;

public interface INetworkControllerClient
{
    Task ActivateAsync(NetworkActivationRequest request, CancellationToken cancellationToken = default);
}
