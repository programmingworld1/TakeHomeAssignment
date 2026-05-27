using WIFIService.Domain.ValueObjects;

namespace WIFIService.Domain.Entities;

public class ServiceOrderItem
{
    public string Id { get; private set; }
    public string? ServiceId { get; private set; }
    public ServiceSpecification ServiceSpecification { get; private set; }
    public List<ServiceCharacteristic> ServiceCharacteristics { get; private set; } = [];

    public ServiceOrderItem(
        string id,
        string? serviceId,
        ServiceSpecification specification,
        List<ServiceCharacteristic> characteristics)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        ArgumentNullException.ThrowIfNull(specification);
        ArgumentNullException.ThrowIfNull(characteristics);

        Id = id;
        ServiceId = serviceId;
        ServiceSpecification = specification;
        ServiceCharacteristics = characteristics;
    }
}
