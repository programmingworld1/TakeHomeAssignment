using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.Tests.Builders;

public class ServiceCharacteristicDtoBuilder
{
    private string _name = "customerId";
    private string _valueType = "string";
    private Dictionary<string, string> _value = new() { ["customerId"] = "CUST-123" };

    public ServiceCharacteristicDtoBuilder WithName(string name) { _name = name; return this; }
    public ServiceCharacteristicDtoBuilder WithValueType(string valueType) { _valueType = valueType; return this; }
    public ServiceCharacteristicDtoBuilder WithValue(Dictionary<string, string> value) { _value = value; return this; }

    public ServiceCharacteristicDto Build() => new(_name, _valueType, _value);
}
