namespace WIFIService.Application.Clients.NetworkInfrastructure;

public interface INetworkInfrastructureClient
{
    Task<IEnumerable<SpeedProfile>> GetSpeedProfilesAsync(CancellationToken cancellationToken = default);
}
