using Mapster;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Contracts.WifiProvisioning.EnableWifi;

namespace WIFIService.Api.Mappings;

public class EnableWifiMappingContractDtoProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EnableWifiRequest, EnableWifiServiceDto>()
            .Map(dest => dest.ServiceOrder, src => new ServiceOrderDto(
                src.ExternalId,
                src.Description,
                src.OrderItem.Id,
                src.OrderItem.Service.Id,
                src.OrderItem.Service.ServiceSpecification.Id,
                src.OrderItem.Service.ServiceSpecification.Name
            ))
            .Map(dest => dest.ServiceCharacteristics, src => src.OrderItem.Service.ServiceCharacteristic
                .Select(c => new ServiceCharacteristicDto(c.Name, c.ValueType, c.Value))
                .ToList());
    }
}
