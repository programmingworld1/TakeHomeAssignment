using WIFIService.Domain.Entities;
using WIFIService.Domain.ValueObjects;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public class EnableWifiService : IEnableWifiService
{
    public Task ExecuteAsync(EnableWifiServiceInput input, CancellationToken cancellationToken = default)
    {
        var characteristics = input.ServiceCharacteristics
            .Select(c => new ServiceCharacteristic(c.Name, c.ValueType, c.Value))
            .ToList();

        var orderItem = ServiceOrderItem.Create(
            id: input.OrderItemId,
            serviceId: input.ServiceId,
            specification: new ServiceSpecification(input.ServiceSpecificationId, input.ServiceSpecificationName),
            characteristics: characteristics
        );

        var serviceOrder = ServiceOrder.Create(
            externalId: input.ExternalId,
            description: input.Description,
            orderItem: orderItem
        );

        return Task.CompletedTask;
    }
}
