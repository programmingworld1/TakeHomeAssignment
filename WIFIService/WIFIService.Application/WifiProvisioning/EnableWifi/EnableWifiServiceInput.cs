namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public record EnableWifiServiceInput(
    string ExternalId,
    string Description,
    string OrderItemId,
    string ServiceId,
    string ServiceSpecificationId,
    string ServiceSpecificationName,
    List<ServiceCharacteristicInput> ServiceCharacteristics
);

public record ServiceCharacteristicInput(
    string Name,
    string ValueType,
    Dictionary<string, string> Value
);
