using WIFIService.Application.Responses;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public interface IEnableWifiService
{
    Task<Result> ExecuteAsync(EnableWifiServiceDto input, CancellationToken cancellationToken = default);
}
