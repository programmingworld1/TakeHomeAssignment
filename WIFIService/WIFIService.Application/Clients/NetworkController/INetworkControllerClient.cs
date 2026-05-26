namespace WIFIService.Application.Clients.NetworkController;

public interface INetworkControllerClient
{
    Task ActivateAsync(NetworkActivationRequest request, CancellationToken cancellationToken = default);
}
