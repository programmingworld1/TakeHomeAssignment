using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Application.Clients.NetworkInfrastructure;
using WIFIService.Application.Mappings;
using WIFIService.Application.Tests.Builders;
using WIFIService.Application.WifiProvisioning.EnableWifi;

namespace WIFIService.Application.Tests.WifiProvisioning.EnableWifi;

public class EnableWifiServiceTests
{
    private readonly INetworkInfrastructureClient _networkInfrastructureClient;
    private readonly INetworkControllerClient _networkControllerClient;
    private readonly EnableWifiService _enableWifiService;

    public EnableWifiServiceTests()
    {
        _networkInfrastructureClient = Substitute.For<INetworkInfrastructureClient>();
        _networkControllerClient = Substitute.For<INetworkControllerClient>();

        var config = new TypeAdapterConfig();
        new EnableWifiMappingDtoDomainProfile().Register(config);
        var mapper = new ServiceMapper(Substitute.For<IServiceProvider>(), config);

        _enableWifiService = new EnableWifiService(
            _networkInfrastructureClient,
            _networkControllerClient,
            mapper,
            Substitute.For<ILogger<EnableWifiService>>());
    }

    [Fact]
    public async Task ActivateAsync_WhenCalledWithValidData_ActivationSucceeds()
    {
        var input = new EnableWifiServiceDtoBuilder().Build();

        var speedProfiles = new List<SpeedProfile>
        {
            new SpeedProfile("FAST-100", DownloadSpeedMbps: 100, UploadSpeedMbps: 50)
        };

        _networkInfrastructureClient
            .GetSpeedProfilesAsync(Arg.Any<CancellationToken>())
            .Returns(speedProfiles);

        var expectedRequest = new NetworkActivationRequest(
            CustomerId: "CUST-123",
            CustomerAddress: "123 Main St",
            UpstreamSpeed: speedProfiles[0].UploadSpeedMbps,
            DownstreamSpeed: speedProfiles[0].DownloadSpeedMbps
        );

        await _enableWifiService.ActivateAsync(input);

        await _networkControllerClient.Received(1).ActivateAsync(expectedRequest, Arg.Any<CancellationToken>());
    }
}
