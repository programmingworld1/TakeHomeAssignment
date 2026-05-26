namespace WIFIService.Domain.ValueObjects;

public record ServiceCharacteristic
{
    public string Name { get; }
    public string ValueType { get; }
    public Dictionary<string, string> Value { get; }

    private ServiceCharacteristic() { }

    public ServiceCharacteristic(string name, string valueType, Dictionary<string, string> value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(valueType))
            throw new ArgumentException("ValueType cannot be empty.", nameof(valueType));
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        Name = name;
        ValueType = valueType;
        Value = value;
    }
}
