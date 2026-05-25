using FluentValidation;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public class EnableWifiRequestValidator : AbstractValidator<EnableWifiServiceDto>
{
    private static readonly string[] RequiredCharacteristics = ["customerId", "customerAddress", "speedProfile"];

    public EnableWifiRequestValidator()
    {
        RuleFor(x => x.ServiceOrder.ExternalId)
            .NotEmpty();

        RuleFor(x => x.ServiceOrder.OrderItemId)
            .NotEmpty();

        RuleFor(x => x.ServiceOrder.ServiceSpecificationId)
            .NotEmpty();

        RuleFor(x => x.ServiceCharacteristics)
            .NotEmpty();

        foreach (var characteristic in RequiredCharacteristics)
        {
            RuleFor(x => x.ServiceCharacteristics)
                .Must(c => c.Any(x => x.Name == characteristic))
                .WithMessage($"ServiceCharacteristic '{characteristic}' is required.");
        }
    }
}
