namespace WIFIService.Application.WifiProvisioning.EnableWifi.Models;

public record ServiceCharacteristicDto(
    string Name,
    string ValueType,
    Dictionary<string, string> Value
);
