namespace WIFIService.Infrastructure.External.NetworkInfrastructure;

public record NetworkInfrastructureResponse(
    string RequestId,
    List<SpeedProfileResponse> SpeedProfiles
);

public record SpeedProfileResponse(
    string Code,
    int DownloadSpeedMbps,
    int UploadSpeedMbps
);
