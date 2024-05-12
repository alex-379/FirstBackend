using AutoMapper;
using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Business.Models.Devices.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Business.Models.Devices;

public class DevicesMappingProfile : Profile
{
    public DevicesMappingProfile()
    {
        CreateMap<CreateDeviceRequest, DeviceDto>();

        CreateMap<DeviceDto, DeviceResponse>();
        CreateMap<DeviceDto, DeviceFullResponse>();
    }
}
