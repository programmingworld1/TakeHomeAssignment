namespace WIFIService.Domain.ValueObjects;

public record ServiceSpecification
{
    public string Id { get; }
    public string Name { get; }

    public ServiceSpecification(
        string id,
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name;
    }
}
