using AutoMapper;
using FirstBackend.Business.Models.Devices;
using FirstBackend.Business.Services;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exсeptions;
using FirstBackend.DataLayer.Interfaces;
using FluentAssertions;
using Moq;

namespace FirstBackend.Business.Tests.Services;

public class DevicesServiceTest
{
    private readonly Mock<IDevicesRepository> _devicesRepositoryMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mapper _mapper;

    public DevicesServiceTest()
    {
        _devicesRepositoryMock = new Mock<IDevicesRepository>();
        _ordersRepositoryMock = new Mock<IOrdersRepository>();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DevicesMappingProfile());
        });

        _mapper = new Mapper(config);
    }

    [Fact]
    public void AddDevice_CreateDeviceRequestSent_GuidReceieved()
    {
        //arange
        var CreateDeviceRequest = TestsData.GetFakeCreateDeviceRequest();
        var expectedGuid = Guid.NewGuid();
        _devicesRepositoryMock.Setup(r => r.AddDevice(It.IsAny<DeviceDto>())).Returns(expectedGuid);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.AddDevice(CreateDeviceRequest);

        //assert
        Assert.Equal(expectedGuid, actual);
    }

    [Fact]
    public void GetDevices_Calles_ListDeviceResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeListDeviceResponse();
        var expectedDevices = TestsData.GetFakeListDeviceDto();
        _devicesRepositoryMock.Setup(x => x.GetDevices()).Returns(expectedDevices);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetDevices();

        //assert
        actual.Should().BeEquivalentTo(expected);
        _devicesRepositoryMock.Verify(m => m.GetDevices(), Times.Once);
    }

    [Fact]
    public void GetDeviceById_GuidSent_DeviceFullResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeDeviceFullResponse();
        var expectedDevice = TestsData.GetFakeDeviceDto();
        var deviceId = Guid.NewGuid();
        _devicesRepositoryMock.Setup(x => x.GetDeviceById(deviceId)).Returns(expectedDevice);
        var orders = TestsData.GetFakeListOrderDto();
        _devicesRepositoryMock.Setup(x => x.GetOrdersByDeviceId(deviceId)).Returns(orders);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetDeviceById(deviceId);

        //assert
        actual.Should().BeEquivalentTo(expected);
        _devicesRepositoryMock.Verify(m => m.GetDeviceById(deviceId), Times.Once);
    }

    [Fact]
    public void GetDeviceByIdNoDevice_EmptyGuidSent_DeviceNotFoundErrorReceieved()
    {
        //arrange
        var deviceId = Guid.Empty;
        _devicesRepositoryMock.Setup(x => x.GetDeviceById(deviceId)).Returns((DeviceDto)null);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.GetDeviceById(deviceId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(DevicesServiceExceptions.NotFoundException, deviceId));
        _devicesRepositoryMock.Verify(m => m.GetDeviceById(deviceId), Times.Once);
    }

    [Fact]
    public void GetDevicesByUserId_GuidSent_ListDeviceResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeListDeviceResponse();
        var expectedDevices = TestsData.GetFakeListDeviceDto();
        var userId = Guid.NewGuid();
        _devicesRepositoryMock.Setup(x => x.GetDevicesByUserId(userId)).Returns(expectedDevices);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.GetDevicesByUserId(userId);

        //assert
        actual.Should().BeEquivalentTo(expected);
        _devicesRepositoryMock.Verify(m => m.GetDevicesByUserId(userId), Times.Once);
    }

    [Fact]
    public void DeleteDeviceById_GuidSent_NoErrorsReceieved()
    {
        //arrange
        var deviceId = Guid.NewGuid();
        var deviceDto = TestsData.GetFakeDeviceDto();
        _devicesRepositoryMock.Setup(x => x.GetDeviceById(deviceId)).Returns(deviceDto);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        sut.DeleteDeviceById(deviceId);

        //assert
        _devicesRepositoryMock.Verify(m => m.GetDeviceById(deviceId), Times.Once);
        _devicesRepositoryMock.Verify(m => m.UpdateDevice(deviceDto), Times.Once);
    }

    [Fact]
    public void DeleteDeviceById_EmptyGuidSent_DeviceNotFoundErrorReceieved()
    {
        //arrange
        var deviceId = Guid.Empty;
        _devicesRepositoryMock.Setup(x => x.GetDeviceById(deviceId)).Returns((DeviceDto)null);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        Action act = () => sut.DeleteDeviceById(deviceId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(DevicesServiceExceptions.NotFoundException, deviceId));
        _devicesRepositoryMock.Verify(m => m.GetDeviceById(deviceId), Times.Once);
        _devicesRepositoryMock.Verify(m => m.UpdateDevice(It.IsAny<DeviceDto>()), Times.Never);
    }
}