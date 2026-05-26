using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WIFIService.Api.Middlewares.Errors;
using WIFIService.Application.WifiProvisioning.EnableWifi;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Contracts.WifiProvisioning.EnableWifi;

namespace WIFIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WifiProvisioningController : ControllerBase
{
    private readonly IValidator<EnableWifiRequest> _validator;
    private readonly IEnableWifiService _enableWifiService;
    private readonly IMapper _mapper;
    private readonly BusinessProblemDetailsFactory _problemDetailsFactory;

    public WifiProvisioningController(
        IValidator<EnableWifiRequest> validator,
        IEnableWifiService enableWifiService,
        IMapper mapper,
        BusinessProblemDetailsFactory problemDetailsFactory)
    {
        _validator = validator;
        _enableWifiService = enableWifiService;
        _mapper = mapper;
        _problemDetailsFactory = problemDetailsFactory;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EnableWifiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Enable([FromBody] EnableWifiRequest request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var input = _mapper.Map<EnableWifiServiceDto>(request);

        var result = await _enableWifiService.ExecuteAsync(input, cancellationToken);

        if (!result.IsSuccess)
        {
            var pd = _problemDetailsFactory.Create(HttpContext, result.Error!);
            return new ObjectResult(pd) { StatusCode = pd.Status };
        }

        return Ok(new EnableWifiResponse("WiFi has been activated successfully."));
    }
}
