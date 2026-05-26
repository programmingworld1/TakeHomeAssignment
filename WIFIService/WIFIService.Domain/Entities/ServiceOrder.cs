using WIFIService.Domain.ValueObjects;

namespace WIFIService.Domain.Entities;

public class ServiceOrder
{
    public Guid Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ServiceOrderStatus Status { get; private set; }
    public ServiceOrderItem OrderItem { get; private set; }

    public ServiceOrder(string externalId, string description, ServiceOrderItem orderItem)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(externalId);
        ArgumentNullException.ThrowIfNull(orderItem);

        Id = Guid.NewGuid();
        ExternalId = externalId;
        Description = description;
        Status = ServiceOrderStatus.Pending;
        OrderItem = orderItem;
    }

    public void MarkAsActive()
    {
        Status = ServiceOrderStatus.Active;
    }
}
