using Microsoft.AspNetCore.Mvc;
using WIFIService.Application.WifiProvisioning.EnableWifi;
using WIFIService.Contracts.WifiProvisioning.EnableWifi;

namespace WIFIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceOrderController : ControllerBase
{
    private readonly IEnableWifiService _enableWifiService;

    public ServiceOrderController(IEnableWifiService enableWifiService)
    {
        _enableWifiService = enableWifiService;
    }

    [HttpPost]
    public async Task<IActionResult> Enable([FromBody] EnableWifiRequest request, CancellationToken cancellationToken)
    {
        var input = new EnableWifiServiceInput(
            ExternalId: request.ExternalId,
            Description: request.Description,
            OrderItemId: request.OrderItem.Id,
            ServiceId: request.OrderItem.Service.Id,
            ServiceSpecificationId: request.OrderItem.Service.ServiceSpecification.Id,
            ServiceSpecificationName: request.OrderItem.Service.ServiceSpecification.Name,
            ServiceCharacteristics: request.OrderItem.Service.ServiceCharacteristic
                .Select(c => new ServiceCharacteristicInput(c.Name, c.ValueType, c.Value))
                .ToList()
        );

        await _enableWifiService.ExecuteAsync(input, cancellationToken);

        return Ok();
    }
}
