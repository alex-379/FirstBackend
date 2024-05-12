using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices;
using FirstBackend.Business.Models.Devices.Requests;
using FirstBackend.Business.Models.Users;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Services;
using FirstBackend.Business.Tests.Fixture;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Moq;

namespace FirstBackend.Business.Tests.Services;

public class DeviceServiceTest
{
    private readonly Mock<IDevicesRepository> _devicesRepositoryMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mapper _mapper;

    public DeviceServiceTest()
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
    public void AddDeviceTest_CreateDeviceRequestSent_GuidReceieved()
    {
        //arange
        var validCreateDeviceRequest = new CreateDeviceRequest()
        {
            Name = "TestDevice",
            Type = DeviceType.Laptop,
            Address = "Khabarovsk"
        };
        var expectedGuid = Guid.NewGuid();
        _devicesRepositoryMock.Setup(r => r.AddDevice(It.IsAny<DeviceDto>())).Returns(expectedGuid);
        var sut = new DevicesService(_devicesRepositoryMock.Object, _ordersRepositoryMock.Object, _mapper);

        //act
        var actual = sut.AddDevice(validCreateDeviceRequest);

        //assert
        Assert.Equal(expectedGuid, actual);
        //_usersRepositoryMock.Verify(r => r.AddUser(It.IsAny<UserDto>()), Times.Once);
    }
}