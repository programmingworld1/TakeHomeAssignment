using FluentValidation;
using WIFIService.Application.Clients;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.WifiProvisioning.EnableWifi;

public class EnableWifiService : IEnableWifiService
{
    private readonly IValidator<EnableWifiServiceDto> _validator;
    private readonly INetworkInfrastructureClient _networkInfrastructureClient;
    private readonly INetworkControllerClient _networkControllerClient;

    public EnableWifiService(
        IValidator<EnableWifiServiceDto> validator,
        INetworkInfrastructureClient networkInfrastructureClient,
        INetworkControllerClient networkControllerClient)
    {
        _validator = validator;
        _networkInfrastructureClient = networkInfrastructureClient;
        _networkControllerClient = networkControllerClient;
    }

    public async Task ExecuteAsync(EnableWifiServiceDto input, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(input, cancellationToken);

        var customerId = input.ServiceCharacteristics.First(c => c.Name == "customerId").Value["customerId"];
        var customerAddress = input.ServiceCharacteristics.First(c => c.Name == "customerAddress").Value["customerAddress"];
        var speedProfileCode = input.ServiceCharacteristics.First(c => c.Name == "speedProfile").Value["speedProfile"];

        var speedProfiles = await _networkInfrastructureClient.GetSpeedProfilesAsync(cancellationToken);

        var speedProfile = speedProfiles.FirstOrDefault(p => p.Code == speedProfileCode)
            ?? throw new InvalidOperationException($"Speed profile '{speedProfileCode}' not found.");

        var activationRequest = new NetworkActivationRequest(
            CustomerId: customerId,
            CustomerAddress: customerAddress,
            UpstreamSpeed: speedProfile.UploadSpeedMbps,
            DownstreamSpeed: speedProfile.DownloadSpeedMbps
        );

        await _networkControllerClient.ActivateAsync(activationRequest, cancellationToken);
    }
}
