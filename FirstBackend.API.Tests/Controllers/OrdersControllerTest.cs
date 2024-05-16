using FirstBackend.API.Controllers;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Orders.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FirstBackend.API.Tests.Controllers;

public class OrdersControllerTest
{
    private readonly Mock<IOrdersService> _ordersServiceMock;

    public OrdersControllerTest()
    {
        _ordersServiceMock = new Mock<IOrdersService>();
    }

    [Fact]
    public void GetOrders_Called_OkResultReceieved()
    {
        //arrange
        _ordersServiceMock.Setup(x => x.GetOrders()).Returns([]);
        var sut = new OrdersController(_ordersServiceMock.Object);

        //act
        var actual = sut.GetOrders();

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _ordersServiceMock.Verify(m => m.GetOrders(), Times.Once);
    }

    [Fact]
    public void GetOrderById_GuidSent_OkResultReceieved()
    {
        //arrange
        var orderId = new Guid();
        _ordersServiceMock.Setup(x => x.GetOrderById(orderId)).Returns(new OrderFullResponse());
        var sut = new OrdersController(_ordersServiceMock.Object);

        //act
        var actual = sut.GetOrderById(orderId);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _ordersServiceMock.Verify(m => m.GetOrderById(orderId), Times.Once);
    }

    [Fact]
    public void DeleteOrderById_GuidSent_NoContentResultReceieved()
    {
        //arrange
        var orderId = new Guid();
        _ordersServiceMock.Setup(x => x.DeleteOrderById(orderId));
        var sut = new OrdersController(_ordersServiceMock.Object);

        //act
        var actual = sut.DeleteOrderById(orderId);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _ordersServiceMock.Verify(m => m.DeleteOrderById(orderId), Times.Once);
    }
}
