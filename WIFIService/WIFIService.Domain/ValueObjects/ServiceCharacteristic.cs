namespace WIFIService.Domain.ValueObjects;

public record ServiceCharacteristic
{
    public string Name { get; }
    public string ValueType { get; }
    public Dictionary<string, string> Value { get; }

    public ServiceCharacteristic(
        string name,
        string valueType,
        Dictionary<string, string> value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(valueType);
        ArgumentNullException.ThrowIfNull(value);

        Name = name;
        ValueType = valueType;
        Value = value;
    }
}
