namespace WIFIService.Domain.ValueObjects;

public record ServiceCharacteristic(string Name, string ValueType, Dictionary<string, string> Value);
