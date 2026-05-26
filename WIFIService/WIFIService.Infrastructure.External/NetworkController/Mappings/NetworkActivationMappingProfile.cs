using Mapster;
using WIFIService.Application.Clients.NetworkController;
using WIFIService.Infrastructure.External.NetworkController.Models;

namespace WIFIService.Infrastructure.External.NetworkController.Mappings;

public class NetworkActivationMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<NetworkActivationRequest, NetworkActivationPayload>();
    }
}
