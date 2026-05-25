namespace WIFIService.Application.Clients;

public interface INetworkInfrastructureClient
{
    Task<IEnumerable<SpeedProfile>> GetSpeedProfilesAsync(CancellationToken cancellationToken = default);
}
