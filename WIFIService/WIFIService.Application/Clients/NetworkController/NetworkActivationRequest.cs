namespace WIFIService.Application.Clients.NetworkController;

public record NetworkActivationRequest(
    string CustomerId,
    string CustomerAddress,
    int UpstreamSpeed,
    int DownstreamSpeed
);
