using WIFIService.Domain.ValueObjects;

namespace WIFIService.Domain.Entities;

public class ServiceOrder
{
    public Guid Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ServiceOrderStatus Status { get; private set; }
    public ServiceOrderItem OrderItem { get; private set; } = null!;

    private ServiceOrder() { }

    public static ServiceOrder Create(string externalId, string description, ServiceOrderItem orderItem)
    {
        return new ServiceOrder
        {
            Id = Guid.NewGuid(),
            ExternalId = externalId,
            Description = description,
            Status = ServiceOrderStatus.Pending,
            OrderItem = orderItem
        };
    }
}
