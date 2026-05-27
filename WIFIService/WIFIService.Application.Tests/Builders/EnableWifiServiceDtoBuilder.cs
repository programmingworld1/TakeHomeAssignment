using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.Tests.Builders;

public class EnableWifiServiceDtoBuilder
{
    private ServiceOrderDto _serviceOrder = new ServiceOrderDtoBuilder().Build();
    private List<ServiceCharacteristicDto> _characteristics =
    [
        new ServiceCharacteristicDtoBuilder()
            .WithName("customerId")
            .WithValue(new Dictionary<string, string> { ["customerId"] = "CUST-123" })
            .Build(),
        new ServiceCharacteristicDtoBuilder()
            .WithName("customerAddress")
            .WithValue(new Dictionary<string, string> { ["customerAddress"] = "123 Main St" })
            .Build(),
        new ServiceCharacteristicDtoBuilder()
            .WithName("speedProfile")
            .WithValue(new Dictionary<string, string> { ["speedProfile"] = "FAST-100" })
            .Build()
    ];

    public EnableWifiServiceDtoBuilder WithServiceOrder(ServiceOrderDto serviceOrder) { _serviceOrder = serviceOrder; return this; }
    public EnableWifiServiceDtoBuilder WithCharacteristics(List<ServiceCharacteristicDto> characteristics) { _characteristics = characteristics; return this; }

    public EnableWifiServiceDto Build() => new(_serviceOrder, _characteristics);
}
