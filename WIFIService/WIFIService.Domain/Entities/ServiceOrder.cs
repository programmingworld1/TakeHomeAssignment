using WIFIService.Domain.ValueObjects;

namespace WIFIService.Domain.Entities;

public class ServiceOrder
{
    public Guid Id { get; private set; }
    public string ExternalId { get; private set; }
    public string? Description { get; private set; }
    public ServiceOrderStatus Status { get; private set; }
    public ServiceOrderItem OrderItem { get; private set; }

    public ServiceOrder(
        string externalId,
        string? description,
        ServiceOrderItem orderItem)
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
        if (Status == ServiceOrderStatus.Active)
            throw new InvalidOperationException("Service order is already active.");

        Status = ServiceOrderStatus.Active;
    }
}
