namespace WIFIService.Contracts.WifiProvisioning.EnableWifi;

public record EnableWifiRequest(
    string ExternalId,
    string Description,
    OrderItem OrderItem
);
