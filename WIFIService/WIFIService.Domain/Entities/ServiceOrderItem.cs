using WIFIService.Domain.ValueObjects;

namespace WIFIService.Domain.Entities;

public class ServiceOrderItem
{
    public string Id { get; private set; } = string.Empty;
    public string ServiceId { get; private set; } = string.Empty;
    public ServiceSpecification ServiceSpecification { get; private set; } = null!;
    public List<ServiceCharacteristic> ServiceCharacteristics { get; private set; } = [];

    private ServiceOrderItem() { }

    public static ServiceOrderItem Create(string id, string serviceId, ServiceSpecification specification, List<ServiceCharacteristic> characteristics)
    {
        return new ServiceOrderItem
        {
            Id = id,
            ServiceId = serviceId,
            ServiceSpecification = specification,
            ServiceCharacteristics = characteristics
        };
    }
}
