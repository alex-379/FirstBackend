using AutoMapper;
using FirstBackend.Business.Models.Devices;
using FirstBackend.Business.Models.Orders;
using FirstBackend.Business.Models.Orders.Requests;
using FirstBackend.Business.Services;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;
using Moq;

namespace FirstBackend.Business.Tests.Services;

public class OrdersServiceTest
{
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mock<IDevicesRepository> _devicesRepositoryMock;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mapper _mapper;

    public OrdersServiceTest()
    {
        _ordersRepositoryMock = new Mock<IOrdersRepository>();
        _devicesRepositoryMock = new Mock<IDevicesRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new OrdersMappingProfile());
            cfg.AddProfile(new DevicesMappingProfile());
        });

        _mapper = new Mapper(config);
    }

    [Fact]
    public void AddOrderTest_ValidCreateOrderRequestSent_GuidReceieved()
    {
        //arrange
        var device1 = Guid.NewGuid();
        var device2 = Guid.NewGuid();
        var customer = Guid.NewGuid();
        var validCreateOrderRequest = new CreateOrderRequest()
        {
            Description = "TestOrder",
            Devices = [
                new()
                {
                    DeviceId = device1,
                    NumberDevices = 1,
                },
                new()
                {
                    DeviceId = device2,
                }]
        };

        var expectedDevices = new List<DeviceDto>()
        {
            new()
            {
                Id = device1,
            },
            new()
            {
                Id = device2,
            }
        };
        _devicesRepositoryMock.Setup(r => r.GetDevices()).Returns(expectedDevices);

        var expectedCustomer = new UserDto()
        {
            Id = customer,
        };
        _usersRepositoryMock.Setup(r => r.GetUserById(customer)).Returns(expectedCustomer);

        var expectedGuid = Guid.NewGuid();
        _ordersRepositoryMock.Setup(r => r.AddOrder(It.IsAny<OrderDto>())).Returns(expectedGuid);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.AddOrder(validCreateOrderRequest, customer);

        //assert
        Assert.Equal(expectedGuid, actual);
    }
}
