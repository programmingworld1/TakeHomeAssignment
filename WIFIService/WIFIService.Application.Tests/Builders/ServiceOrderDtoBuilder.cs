using WIFIService.Application.WifiProvisioning.EnableWifi.Models;

namespace WIFIService.Application.Tests.Builders;

public class ServiceOrderDtoBuilder
{
    private string _externalId = "EXT-001";
    private string _description = "Test order";
    private string _orderItemId = "ITEM-001";
    private string _serviceId = "SVC-001";
    private string _serviceSpecificationId = "SPEC-001";
    private string _serviceSpecificationName = "Basic";

    public ServiceOrderDtoBuilder WithExternalId(string externalId)
    {
        _externalId = externalId;
        return this;
    }

    public ServiceOrderDtoBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ServiceOrderDtoBuilder WithOrderItemId(string orderItemId)
    {
        _orderItemId = orderItemId;
        return this;
    }

    public ServiceOrderDtoBuilder WithServiceId(string serviceId)
    {
        _serviceId = serviceId;
        return this;
    }

    public ServiceOrderDtoBuilder WithServiceSpecificationId(string id)
    {
        _serviceSpecificationId = id;
        return this;
    }

    public ServiceOrderDtoBuilder WithServiceSpecificationName(string name)
    {
        _serviceSpecificationName = name;
        return this;
    }

    public ServiceOrderDto Build()
    {
        return new ServiceOrderDto(
            _externalId, 
            _description, 
            _orderItemId, 
            _serviceId, 
            _serviceSpecificationId, 
            _serviceSpecificationName);
    }
}
