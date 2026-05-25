namespace WIFIService.Application.WifiProvisioning.EnableWifi.Models;

public record EnableWifiServiceDto(
    ServiceOrderDto ServiceOrder,
    List<ServiceCharacteristicDto> ServiceCharacteristics
);
