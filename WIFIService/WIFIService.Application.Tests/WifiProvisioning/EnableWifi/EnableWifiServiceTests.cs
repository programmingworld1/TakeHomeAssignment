using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Application.Clients.NetworkInfrastructure;
using WIFIService.Application.Mappings;
using WIFIService.Application.WifiProvisioning.EnableWifi;
using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.Tests.WifiProvisioning.EnableWifi;

public class EnableWifiServiceTests
{
    private readonly INetworkInfrastructureClient _networkInfrastructureClient;
    private readonly INetworkControllerClient _networkControllerClient;
    private readonly EnableWifiService _sut;

    public EnableWifiServiceTests()
    {
        _networkInfrastructureClient = Substitute.For<INetworkInfrastructureClient>();
        _networkControllerClient = Substitute.For<INetworkControllerClient>();

        var config = new TypeAdapterConfig();
        new EnableWifiMappingDtoDomainProfile().Register(config);
        var mapper = new ServiceMapper(Substitute.For<IServiceProvider>(), config);

        _sut = new EnableWifiService(_networkInfrastructureClient, _networkControllerClient, mapper,
            Substitute.For<ILogger<EnableWifiService>>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenActivationCalledWithCorrectData_ActivationSucceeds()
    {
        var customerId = "CUST-123";
        var customerAddress = "123 Main St";
        var speedProfileCode = "FAST-100";

        var serviceOrder = new ServiceOrderDto("EXT-001", "Test order", "ITEM-001", "SVC-001", "SPEC-001", "Basic");

        var characteristics = new List<ServiceCharacteristicDto>
        {
            new ServiceCharacteristicDto(
                "customerId",
                "string",
                new Dictionary<string, string> { ["customerId"] = customerId }
            ),
            new ServiceCharacteristicDto(
                "customerAddress",
                "string",
                new Dictionary<string, string> { ["customerAddress"] = customerAddress }
            ),
            new ServiceCharacteristicDto(
                "speedProfile",
                "string",
                new Dictionary<string, string> { ["speedProfile"] = speedProfileCode }
            )
        };

        var input = new EnableWifiServiceDto(serviceOrder, characteristics);

        var speedProfiles = new List<SpeedProfile>
        {
            new SpeedProfile(speedProfileCode, DownloadSpeedMbps: 100, UploadSpeedMbps: 50)
        };

        _networkInfrastructureClient
            .GetSpeedProfilesAsync(Arg.Any<CancellationToken>())
            .Returns(speedProfiles);

        var expectedRequest = new NetworkActivationRequest(
            CustomerId: customerId,
            CustomerAddress: customerAddress,
            UpstreamSpeed: speedProfiles[0].UploadSpeedMbps,
            DownstreamSpeed: speedProfiles[0].DownloadSpeedMbps
        );

        await _sut.ExecuteAsync(input);

        await _networkControllerClient.Received(1).ActivateAsync(expectedRequest, Arg.Any<CancellationToken>());
    }
}
