namespace WIFIService.Application.Clients.NetworkInfrastructure;

public record SpeedProfile(
    string Code,
    int DownloadSpeedMbps,
    int UploadSpeedMbps
);
