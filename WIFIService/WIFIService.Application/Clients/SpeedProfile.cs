namespace WIFIService.Application.Clients;

public record SpeedProfile(
    string Code,
    int DownloadSpeedMbps,
    int UploadSpeedMbps
);
