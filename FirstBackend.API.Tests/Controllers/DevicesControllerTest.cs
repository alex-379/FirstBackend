using FirstBackend.API.Controllers;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Business.Models.Devices.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FirstBackend.API.Tests.Controllers;

public class DevicesControllerTest
{
    private readonly Mock<IDevicesService> _devicesServiceMock;

    public DevicesControllerTest()
    {
        _devicesServiceMock = new Mock<IDevicesService>();
    }

    [Fact]
    public void GetDevices_Called_OkResultReceieved()
    {
        //arrange
        _devicesServiceMock.Setup(x => x.GetDevices()).Returns([]);
        var sut = new DevicesController(_devicesServiceMock.Object);

        //act
        var actual = sut.GetDevices();

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _devicesServiceMock.Verify(m => m.GetDevices(), Times.Once);
    }

    [Fact]
    public void GetDeviceById_GuidSent_OkResultReceieved()
    {
        //arrange
        var deviceId = new Guid();
        _devicesServiceMock.Setup(x => x.GetDeviceById(deviceId)).Returns(new DeviceFullResponse());
        var sut = new DevicesController(_devicesServiceMock.Object);

        //act
        var actual = sut.GetDeviceById(deviceId);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _devicesServiceMock.Verify(m => m.GetDeviceById(deviceId), Times.Once);
    }

    [Fact]
    public void CreateDevice_CreateDeviceRequestSent_CreatedResultReceieved()
    {
        //arrange
        var createDeviceRequest = new CreateDeviceRequest();
        _devicesServiceMock.Setup(x => x.AddDevice(createDeviceRequest)).Returns(new Guid());
        var sut = new DevicesController(_devicesServiceMock.Object);

        //act
        var actual = sut.CreateDevice(createDeviceRequest);

        //assert
        actual.Result.Should().BeOfType<CreatedResult>();
        _devicesServiceMock.Verify(m => m.AddDevice(createDeviceRequest), Times.Once);
    }

    [Fact]
    public void DeleteDeviceById_GuidSent_NoContentResultReceieved()
    {
        //arrange
        var deviceId = new Guid();
        _devicesServiceMock.Setup(x => x.DeleteDeviceById(deviceId));
        var sut = new DevicesController(_devicesServiceMock.Object);

        //act
        var actual = sut.DeleteDeviceById(deviceId);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _devicesServiceMock.Verify(m => m.DeleteDeviceById(deviceId), Times.Once);
    }
}
