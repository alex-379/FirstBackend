using AutoMapper;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Models.Orders.Responses;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Constants.Logs.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;
using System.Data;

namespace FirstBackend.Business.Services;

public class OrdersService(IOrdersRepository ordersRepository, IDevicesRepository devicesRepository, IUsersRepository usersRepository, IMapper mapper) : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository = ordersRepository;
    private readonly IDevicesRepository _devicesRepository = devicesRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = Log.ForContext<OrdersService>();

    public Guid AddOrder(CreateOrderRequest request)
    {
        var devices = _devicesRepository.GetAllDevices()
            .Where(d => request.Devices
                .Select(o => o.DeviceId)
                .Contains(d.Id))
            .ToList();

        if (devices.Count != request.Devices.Count)
        {
            throw new NotFoundException(DevicesServiceExceptions.NotFoundExceptionDevices);
        }

        var order = _mapper.Map<OrderDto>(request);
        order.Devices = devices;
        order.Customer = _usersRepository.GetUserById(request.Customer)
            ?? throw new NotFoundException(string.Format(UsersServiceExceptions.NotFoundException, request.Customer));
        order.DevicesOrders = _mapper.Map<List<DevicesOrders>>(request.Devices);
        _logger.Information(OrdersServiceLogs.AddOrder, order.Id);
        order.Id = _ordersRepository.AddOrder(order);

        return order.Id;
    }

    public List<OrderResponse> GetAllOrders()
    {
        _logger.Information(OrdersServiceLogs.GetAllOrders);
        var orders = _mapper.Map<List<OrderResponse>>(_ordersRepository.GetAllOrders());

        return orders;
    }

    public OrderFullResponse GetOrderById(Guid id)
    {
        _logger.Information(OrdersServiceLogs.GetOrderById, id);
        var order = _ordersRepository.GetOrderById(id)
            ?? throw new NotFoundException(string.Format(OrdersServiceExceptions.NotFoundException, id));
        var orderResponse = _mapper.Map<OrderFullResponse>(order);

        return orderResponse;
    }

    public List<OrdersByUserResponse> GetOrdersByUserId(Guid userId)
    {
        _logger.Information(OrdersServiceLogs.GetOrdersByUserId, userId);
        var orders = _ordersRepository.GetOrdersByUserId(userId);
        var ordersResponse = _mapper.Map<List<OrdersByUserResponse>>(orders);

        return ordersResponse;
    }

    public void DeleteOrderById(Guid id)
    {
        _logger.Information(OrdersServiceLogs.CheckOrderById, id);
        var order = _ordersRepository.GetOrderById(id)
            ?? throw new NotFoundException(string.Format(OrdersServiceExceptions.NotFoundException, id));
        _logger.Information(OrdersServiceLogs.SetIsDeletedOrderById, id);
        order.IsDeleted = true;
        _logger.Information(OrdersServiceLogs.UpdateOrderById, id);
        _ordersRepository.UpdateOrder(order);
    }
}