using AutoMapper;
using FirstBackend.Business.Models.Devices;
using FirstBackend.Business.Models.Orders;
using FirstBackend.Business.Models.Users;
using FirstBackend.Business.Services;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using FluentAssertions;
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
            cfg.AddProfile(new UsersMappingProfile());
        });

        _mapper = new Mapper(config);
    }

    [Fact]
    public void AddOrder_CreateOrderRequestAndGuidCustomerSent_GuidReceieved()
    {
        //arrange
        var createOrderRequestAndGuidCustomer = TestsData.GetFakeCreateOrderRequest();
        var expectedDevices = TestsData.GetFakeListDeviceDto();
        _devicesRepositoryMock.Setup(x => x.GetDevices()).Returns(expectedDevices);
        var customerId = Guid.NewGuid();
        var expectedCustomer = new UserDto()
        {
            Id = customerId,
        };
        _usersRepositoryMock.Setup(r => r.GetUserById(customerId)).Returns(expectedCustomer);
        var expectedGuid = Guid.NewGuid();
        _ordersRepositoryMock.Setup(x => x.AddOrder(It.IsAny<OrderDto>())).Returns(expectedGuid);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.AddOrder(createOrderRequestAndGuidCustomer, customerId);

        //assert
        Assert.Equal(expectedGuid, actual);
        _devicesRepositoryMock.Verify(m => m.GetDevices(), Times.Once);
        _usersRepositoryMock.Verify(m => m.GetUserById(customerId), Times.Once);
        _ordersRepositoryMock.Verify(m => m.AddOrder(It.IsAny<OrderDto>()), Times.Once);
    }

    [Fact]
    public void AddOrderNotAllDevicesInDatabase_CreateOrderRequestNotAllDevicesAndGuidCustomerSent_DevicesNotFoundErrorReceieved()
    {
        //arrange
        var createOrderRequestNotAllDevicesAndGuidCustomer = TestsData.GetFakeCreateOrderRequest();
        _devicesRepositoryMock.Setup(x => x.GetDevices()).Returns([]);
        var customerId = Guid.NewGuid();
        var expectedCustomer = new UserDto()
        {
            Id = customerId,
        };
        _usersRepositoryMock.Setup(x => x.GetUserById(customerId)).Returns(expectedCustomer);
        var expectedGuid = Guid.NewGuid();
        _ordersRepositoryMock.Setup(x => x.AddOrder(It.IsAny<OrderDto>())).Returns(expectedGuid);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.AddOrder(createOrderRequestNotAllDevicesAndGuidCustomer, customerId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(DevicesServiceExceptions.NotFoundExceptionDevices);
        _devicesRepositoryMock.Verify(m => m.GetDevices(), Times.Once);
        _usersRepositoryMock.Verify(m => m.GetUserById(customerId), Times.Never);
        _ordersRepositoryMock.Verify(m => m.AddOrder(It.IsAny<OrderDto>()), Times.Never);
    }

    [Fact]
    public void AddOrderNoUser_CreateOrderRequestAndEmptyGuidCustomerSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var createOrderRequestNotAllDevicesAndGuidCustomer = TestsData.GetFakeCreateOrderRequest();
        var expectedDevices = TestsData.GetFakeListDeviceDto();
        _devicesRepositoryMock.Setup(x => x.GetDevices()).Returns(expectedDevices);
        var customerId = Guid.Empty;
        _usersRepositoryMock.Setup(x => x.GetUserById(customerId)).Returns((UserDto)null);
        var expectedGuid = Guid.NewGuid();
        _ordersRepositoryMock.Setup(x => x.AddOrder(It.IsAny<OrderDto>())).Returns(expectedGuid);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.AddOrder(createOrderRequestNotAllDevicesAndGuidCustomer, customerId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, customerId));
        _devicesRepositoryMock.Verify(m => m.GetDevices(), Times.Once);
        _usersRepositoryMock.Verify(m => m.GetUserById(customerId), Times.Once);
        _ordersRepositoryMock.Verify(m => m.AddOrder(It.IsAny<OrderDto>()), Times.Never);
    }

    [Fact]
    public void GetOrders_Calles_ListOrderResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeListOrderResponse();
        var expectedOrders = TestsData.GetFakeListOrderDto();
        _ordersRepositoryMock.Setup(x => x.GetOrders()).Returns(expectedOrders);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetOrders();

        //assert
        actual.Should().BeEquivalentTo(expected);
        _ordersRepositoryMock.Verify(m => m.GetOrders(), Times.Once);
    }

    [Fact]
    public void GetOrderById_GuidSent_OrderFullResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeOrderFullResponse();
        var expectedOrder = TestsData.GetFakeOrderDto();
        var orderId = Guid.NewGuid();
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns(expectedOrder);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetOrderById(orderId);

        //assert
        actual.Should().BeEquivalentTo(expected);
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
    }

    [Fact]
    public void GetOrderByIdNoOrder_EmptyGuidSent_OrderNotFoundErrorReceieved()
    {
        //arrange
        var orderId = Guid.Empty;
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns((OrderDto)null);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.GetOrderById(orderId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(OrdersServiceExceptions.NotFoundException, orderId));
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
    }

    [Fact]
    public void GetOrdersByUserId_GuidSent_ListOrderByUserResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeListOrderResponse();
        var expectedOrders = TestsData.GetFakeListOrderDto();
        var userId = Guid.NewGuid();
        _ordersRepositoryMock.Setup(x => x.GetOrdersByUserId(userId)).Returns(expectedOrders);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetOrdersByUserId(userId);

        //assert
        actual.Should().BeEquivalentTo(expected);
        _ordersRepositoryMock.Verify(m => m.GetOrdersByUserId(userId), Times.Once);
    }

    [Fact]
    public void DeleteOrderById_GuidSent_NoErrorsReceieved()
    {
        //arrange
        var orderId = Guid.NewGuid();
        var orderDto = TestsData.GetFakeOrderDto();
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns(orderDto);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        sut.DeleteOrderById(orderId);

        //assert
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
        _ordersRepositoryMock.Verify(m => m.UpdateOrder(orderDto), Times.Once);
    }

    [Fact]
    public void DeleteOrderById_EmptyGuidSent_OrderNotFoundErrorReceieved()
    {
        //arrange
        var orderId = Guid.Empty;
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns((OrderDto)null);
        var sut = new OrdersService(_ordersRepositoryMock.Object, _devicesRepositoryMock.Object, _usersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.DeleteOrderById(orderId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(OrdersServiceExceptions.NotFoundException, orderId));
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
        _ordersRepositoryMock.Verify(m => m.UpdateOrder(It.IsAny<OrderDto>()), Times.Never);
    }
}
