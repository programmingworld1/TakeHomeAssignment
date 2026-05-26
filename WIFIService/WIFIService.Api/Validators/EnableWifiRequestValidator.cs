using FluentValidation;
using WIFIService.Contracts.WifiProvisioning.EnableWifi;

namespace WIFIService.Api.Validators;

public class EnableWifiRequestValidator : AbstractValidator<EnableWifiRequest>
{
    public EnableWifiRequestValidator()
    {
        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.OrderItem)
            .NotNull()
            .SetValidator(new OrderItemValidator());
    }
}

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Service)
            .NotNull()
            .SetValidator(new ServiceValidator());
    }
}

public class ServiceValidator : AbstractValidator<Service>
{
    private static readonly string[] RequiredCharacteristics = ["customerId", "customerAddress", "speedProfile"];

    public ServiceValidator()
    {
        RuleFor(x => x.ServiceSpecification.Id)
            .NotEmpty();

        RuleFor(x => x.ServiceCharacteristic)
            .NotNull()
            .NotEmpty();

        foreach (var characteristic in RequiredCharacteristics)
        {
            RuleFor(x => x.ServiceCharacteristic)
                .Must(c => c.Any(x => x.Name == characteristic))
                .WithMessage($"ServiceCharacteristic '{characteristic}' is required.");

            RuleFor(x => x.ServiceCharacteristic)
                .Must(c => c.Where(x => x.Name == characteristic)
                             .All(x => x.Value.ContainsKey(characteristic)))
                .When(c => c.ServiceCharacteristic.Any(x => x.Name == characteristic))
                .WithMessage($"ServiceCharacteristic '{characteristic}' must contain a value with key '{characteristic}'.");
        }
    }
}
