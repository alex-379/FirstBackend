using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Tests.Repositories;

public class OrdersRepositoryTest
{
    private readonly Mock<MainerLxContext> _mainerLxContextMock;

    public OrdersRepositoryTest()
    {
        _mainerLxContextMock = new Mock<MainerLxContext>();
    }

    [Fact]
    public void AddOrder_OrderDtoSent_GuidReceieved()
    {
        //arrange
        var expected = 1;
        var orders = new List<OrderDto>();
        var orderDto = TestsData.GetFakeOrderDto();
        var mock = orders.BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Orders.Add(orderDto))
            .Returns(mock.Object.Add(orderDto))
            .Callback<OrderDto>(orders.Add);
        var sut = new OrdersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.AddOrder(orderDto);

        //assert
        Assert.Equal(expected, orders.Count);
        mock.Verify(m => m.Add(orderDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetOrders_Called_OrderDtoListReceieved()
    {
        //arrange
        var expected = 2;
        _mainerLxContextMock.Setup(x => x.Orders)
            .ReturnsDbSet(TestsData.GetFakeOrderDtoList());
        var sut = new OrdersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetOrders();

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public void GetOrderById_GuidSent_OrderDtoReceieved()
    {
        //arrange
        var expected = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566");
        var mock = TestsData.GetFakeOrderDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Orders)
            .Returns(mock.Object);
        var sut = new OrdersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetOrderById(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }

    [Fact]
    public void GetOrdersByUserId_GuidSent_OrderDtoReceieved()
    {
        //arrange
        var expected = new List<OrderDto>()
        {
            TestsData.GetFakeOrderDtoList()[0]
        };

        var userId = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890");
        var mock = TestsData.GetFakeOrderDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Orders)
            .Returns(mock.Object);
        var sut = new OrdersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetOrdersByUserId(userId);

        //assert
        Assert.NotNull(actual);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UpdateOrder_OrderDtoSent_NoErrorsReceieved()
    {
        //arrange
        var orderDto = TestsData.GetFakeOrderDtoList()[0];
        var mock = Enumerable.Empty<OrderDto>().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Orders.Update(orderDto))
            .Returns(mock.Object.Update(orderDto));
        var sut = new OrdersRepository(_mainerLxContextMock.Object);

        //act
        sut.UpdateOrder(orderDto);

        //assert
        mock.Verify(m => m.Update(orderDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}