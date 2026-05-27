namespace WIFIService.Domain.ValueObjects;

public record ServiceSpecification
{
    public string Id { get; }
    public string Name { get; }

    private ServiceSpecification() { }

    public ServiceSpecification(
        string id,
        string name)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
    }
}
