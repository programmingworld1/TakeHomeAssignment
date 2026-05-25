namespace WIFIService.Contracts.WifiProvisioning.EnableWifi;

public record ServiceCharacteristic(
    string Name,
    string ValueType,
    Dictionary<string, string> Value
);
