using AutoMapper;
using FirstBackend.Buiseness.Interfaces;
using FirstBackend.Buiseness.Models.Orders.Requests;
using FirstBackend.Buiseness.Models.Orders.Responses;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using Serilog;
using System.Data;

namespace FirstBackend.Buiseness.Services;

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
                               .Where(o => request.Devices.Contains(o.Id)).ToList();

        if (devices.Count != request.Devices.Count)
        {
            throw new ValidationDataException("Не все устройста есть в базе");
        }

        var order = _mapper.Map<OrderDto>(request);
        order.Devices = devices;
        order.Customer = _usersRepository.GetUserById(request.Customer);
        _logger.Information($"Обращаемся к методу репозитория Создание нового заказа с ID {order.Id}");
        _ordersRepository.AddOrder(order);

        return order.Id;
    }

    public List<OrderResponse> GetAllOrders()
    {
        _logger.Information($"Обращаемся к методу репозитория Получение всех заказов");
        var orders = _mapper.Map<List<OrderResponse>>(_ordersRepository.GetAllOrders());

        return orders;
    }

    public OrderFullResponse GetOrderById(Guid id)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение заказа по ID {id}");
        var order = _ordersRepository.GetOrderById(id) ?? throw new NotFoundException($"Заказ с ID {id} не найден");
        var orderResponse = _mapper.Map<OrderFullResponse>(order);

        return orderResponse;
    }

    public List<OrdersByUserResponse> GetOrdersByUserId(Guid userId)
    {
        _logger.Information($"Обращаемся к методу репозитория Получение заказа по ID пользователя {userId}");
        var orders = _ordersRepository.GetOrdersByUserId(userId) ?? throw new NotFoundException($"Заказы пользователя с ID {userId} не найдены");
        var ordersResponse = _mapper.Map<List<OrdersByUserResponse>>(orders);

        return ordersResponse;
    }

    public void DeleteOrderById(Guid id)
    {
        _logger.Information($"Проверяем существует ли заказ с ID {id}");
        var order = _ordersRepository.GetOrderById(id) ?? throw new NotFoundException($"Заказ с ID {id} не найден");
        _logger.Information($"Обращаемся к методу репозитория Удаление заказа c ID {id}");
        _ordersRepository.DeleteOrder(order);
    }
}
