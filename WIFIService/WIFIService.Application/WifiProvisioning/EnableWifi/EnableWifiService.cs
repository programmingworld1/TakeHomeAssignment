using MapsterMapper;
using Microsoft.Extensions.Logging;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Application.Clients.NetworkInfrastructure;
using WIFIService.Application.Responses;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;
using WIFIService.Domain.Entities;
using WIFIService.Domain.ValueObjects;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public class EnableWifiService : IEnableWifiService
{
    private readonly INetworkInfrastructureClient _networkInfrastructureClient;
    private readonly INetworkControllerClient _networkControllerClient;
    private readonly IMapper _mapper;
    private readonly ILogger<EnableWifiService> _logger;

    public EnableWifiService(
        INetworkInfrastructureClient networkInfrastructureClient,
        INetworkControllerClient networkControllerClient,
        IMapper mapper,
        ILogger<EnableWifiService> logger)
    {
        _networkInfrastructureClient = networkInfrastructureClient;
        _networkControllerClient = networkControllerClient;
        _mapper = mapper;
        _logger = logger;
    }

    private static string GetCharacteristicValue(List<ServiceCharacteristic> characteristics, string name)
    {
        return characteristics.First(c => c.Name == name).Value[name];
    }

    public async Task<Result<ServiceOrderStatus>> ActivateAsync(EnableWifiServiceDto input, CancellationToken cancellationToken = default)
    {
        var serviceOrder = _mapper.Map<ServiceOrder>(input);
        var characteristics = serviceOrder.OrderItem.ServiceCharacteristics;

        var customerId = GetCharacteristicValue(characteristics, "customerId");
        var customerAddress = GetCharacteristicValue(characteristics, "customerAddress");
        var speedProfileCode = GetCharacteristicValue(characteristics, "speedProfile");

        _logger.LogInformation("Processing WiFi activation for customer {CustomerId} with speed profile {SpeedProfileCode}",
            customerId, speedProfileCode);

        var speedProfiles = await _networkInfrastructureClient.GetSpeedProfilesAsync(cancellationToken);
        var speedProfile = speedProfiles.FirstOrDefault(p => p.Code == speedProfileCode);

        if (speedProfile is null)
        {
            _logger.LogWarning("Speed profile {SpeedProfileCode} not found", 
                speedProfileCode);

            return Result<ServiceOrderStatus>
                .Failure(new Error(ErrorCode.SpeedProfileNotFound, $"Speed profile '{speedProfileCode}' not found."));
        }

        var activationRequest = new NetworkActivationRequest(
            CustomerId: customerId,
            CustomerAddress: customerAddress,
            UpstreamSpeed: speedProfile.UploadSpeedMbps,
            DownstreamSpeed: speedProfile.DownloadSpeedMbps
        );

        await _networkControllerClient.ActivateAsync(activationRequest, cancellationToken);

        serviceOrder.MarkAsActive();

        _logger.LogInformation("WiFi activation succeeded for customer {CustomerId}", customerId);

        return Result<ServiceOrderStatus>.Success(serviceOrder.Status);
    }
}
