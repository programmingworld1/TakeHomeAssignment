namespace WIFIService.Contracts.WifiProvisioning.EnableWifi;

public record Service(
    string Id,
    ServiceSpecification ServiceSpecification,
    List<ServiceCharacteristic> ServiceCharacteristics
);
