using AutoMapper;
using FirstBackend.Buiseness.Models.Orders.Requests;
using FirstBackend.Buiseness.Models.Orders.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Models.Orders;

public class OrdersMappingProfile : Profile
{
    public OrdersMappingProfile()
    {
        CreateMap<CreateOrderRequest, OrderDto>();

        CreateMap<OrderDto, OrderResponse>();
    }
}
