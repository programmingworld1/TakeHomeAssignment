using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WIFIService.Application.WifiProvisioning.EnableWifi;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Contracts.WifiProvisioning.EnableWifi;

namespace WIFIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WifiProvisioningController : ControllerBase
{
    private readonly IEnableWifiService _enableWifiService;
    private readonly IMapper _mapper;

    public WifiProvisioningController(IEnableWifiService enableWifiService, IMapper mapper)
    {
        _enableWifiService = enableWifiService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Enable([FromBody] EnableWifiRequest request, CancellationToken cancellationToken)
    {
        var input = _mapper.Map<EnableWifiServiceDto>(request);

        await _enableWifiService.ExecuteAsync(input, cancellationToken);

        return Ok();
    }
}
