namespace WIFIService.Application.Clients;

public record NetworkActivationRequest(
    string CustomerId,
    string CustomerAddress,
    int UpstreamSpeed,
    int DownstreamSpeed
);
