namespace WIFIService.Application.WifiProvisioning.EnableWifi.Models;

public record ServiceOrderDto(
    string ExternalId,
    string Description,
    string OrderItemId,
    string ServiceId,
    string ServiceSpecificationId,
    string ServiceSpecificationName
);
