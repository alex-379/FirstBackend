using AutoMapper;
using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Business.Models.Orders;

public class OrdersMappingProfile : Profile
{
    public OrdersMappingProfile()
    {
        CreateMap<CreateOrderRequest, OrderDto>()
            .ForMember(d => d.Devices, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore());

        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderDto, OrderFullResponse>();
        CreateMap<OrderDto, OrdersByUserResponse>();
    }
}
