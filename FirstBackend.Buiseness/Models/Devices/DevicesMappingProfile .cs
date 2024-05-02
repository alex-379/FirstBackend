using AutoMapper;
using FirstBackend.Buiseness.Models.Devices.Requests;
using FirstBackend.Buiseness.Models.Devices.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Models.Devices;

public class DevicesMappingProfile : Profile
{
    public DevicesMappingProfile()
    {
        CreateMap<CreateDeviceRequest, DeviceDto>();

        CreateMap<DeviceDto, DeviceResponse>();
        CreateMap<DeviceDto, DeviceFullResponse>();
    }
}
