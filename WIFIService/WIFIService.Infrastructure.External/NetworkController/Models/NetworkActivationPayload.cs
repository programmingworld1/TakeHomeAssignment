namespace WIFIService.Infrastructure.External.NetworkController.Models;

public record NetworkActivationPayload(
    string CustomerId,
    string CustomerAddress,
    int UpstreamSpeed,
    int DownstreamSpeed
);
