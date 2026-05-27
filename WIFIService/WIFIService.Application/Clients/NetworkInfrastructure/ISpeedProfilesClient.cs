namespace WIFIService.Application.Clients.NetworkInfrastructure;

public interface ISpeedProfilesClient
{
    Task<IEnumerable<SpeedProfile>> GetSpeedProfilesAsync(CancellationToken cancellationToken = default);
}
