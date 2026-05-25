namespace WIFIService.Application.WifiProvisioning.EnableWifi;
 
public interface IEnableWifiService
{
    Task ExecuteAsync(EnableWifiServiceInput input, CancellationToken cancellationToken = default);
}
