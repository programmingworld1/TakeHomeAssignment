using WIFIService.Application.Responses;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Domain.ValueObjects;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public interface IEnableWifiService
{
    Task<Result<ServiceOrderStatus>> ActivateAsync(EnableWifiServiceDto input, CancellationToken cancellationToken = default);
}
