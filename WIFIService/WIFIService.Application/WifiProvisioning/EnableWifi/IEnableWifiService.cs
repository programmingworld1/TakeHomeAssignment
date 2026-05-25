using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public interface IEnableWifiService
{
    Task ExecuteAsync(EnableWifiServiceDto input, CancellationToken cancellationToken = default);
}
