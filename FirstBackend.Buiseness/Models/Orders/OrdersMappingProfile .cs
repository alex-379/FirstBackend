using AutoMapper;
using FirstBackend.Buiseness.Models.Orders.Requests;
using FirstBackend.Buiseness.Models.Orders.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Models.Orders;

public class OrdersMappingProfile : Profile
{
    public OrdersMappingProfile()
    {
        CreateMap<CreateOrderRequest, OrderDto>()
            .ForMember(d=>d.Devices, o=>o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore());

        CreateMap<OrderDto, OrderResponse>();
        CreateMap<OrderDto, OrderFullResponse>();
        CreateMap<OrderDto, OrdersByUserResponse>();
    }
}
