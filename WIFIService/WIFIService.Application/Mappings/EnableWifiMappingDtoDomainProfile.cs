using Mapster;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Domain.Entities;
using DomainCharacteristic = WIFIService.Domain.ValueObjects.ServiceCharacteristic;
using DomainSpecification = WIFIService.Domain.ValueObjects.ServiceSpecification;

namespace WIFIService.Application.Mappings;

public class EnableWifiMappingDtoDomainProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EnableWifiServiceDto, ServiceOrder>()
            .ConstructUsing(src => new ServiceOrder(
                src.ServiceOrder.ExternalId,
                src.ServiceOrder.Description,
                new ServiceOrderItem(
                    src.ServiceOrder.OrderItemId,
                    src.ServiceOrder.ServiceId,
                    new DomainSpecification(
                        src.ServiceOrder.ServiceSpecificationId,
                        src.ServiceOrder.ServiceSpecificationName),
                    src.ServiceCharacteristics
                        .Select(c => new DomainCharacteristic(c.Name, c.ValueType, c.Value))
                        .ToList()
                )
            ));
    }
}
