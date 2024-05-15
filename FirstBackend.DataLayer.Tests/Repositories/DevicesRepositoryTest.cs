using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Tests.Repositories;

public class DevicesRepositoryTest
{
    private readonly Mock<MainerLxContext> _mainerLxContextMock;

    public DevicesRepositoryTest()
    {
        _mainerLxContextMock = new Mock<MainerLxContext>();
    }

    [Fact]
    public void AddDevice_DeviceDtoSent_GuidReceieved()
    {
        //arrange
        var expected = 1;
        var devices = new List<DeviceDto>();
        var deviceDto = TestsData.GetFakeDeviceDto();
        var mock = devices.BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Devices.Add(deviceDto))
            .Returns(mock.Object.Add(deviceDto))
            .Callback<DeviceDto>(devices.Add);
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.AddDevice(deviceDto);

        //assert
        Assert.Equal(expected, devices.Count);
        mock.Verify(m => m.Add(deviceDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetDevices_Called_DeviceDtoListReceieved()
    {
        //arrange
        var expected = 3;
        _mainerLxContextMock.Setup(x => x.Devices)
            .ReturnsDbSet(TestsData.GetFakeDeviceDtoList());
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetDevices();

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public void GetDeviceById_GuidSent_DeviceDtoReceieved()
    {
        //arrange
        var expected = new Guid("dac7436b-5afd-4038-9ade-0581f7bb4714");
        var mock = TestsData.GetFakeDeviceDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Devices)
            .Returns(mock.Object);
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetDeviceById(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }

    [Fact]
    public void GetDevicesByUserId_GuidSent_OrderDtoReceieved()
    {
        //arrange
        var expected = new List<DeviceDto>()
        {
            TestsData.GetFakeDeviceDtoList()[0],
            TestsData.GetFakeDeviceDtoList()[1]
        };

        var userId = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92894");
        var mock = TestsData.GetFakeDeviceDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Devices)
            .Returns(mock.Object);
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetDevicesByUserId(userId);

        //assert
        Assert.NotNull(actual);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void UpdateDevice_DeviceDtoSent_NoErrorsReceieved()
    {
        //arrange
        var deviceDto = TestsData.GetFakeDeviceDtoList()[2];
        var mock = Enumerable.Empty<DeviceDto>().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Devices.Update(deviceDto))
            .Returns(mock.Object.Update(deviceDto));
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        sut.UpdateDevice(deviceDto);

        //assert
        mock.Verify(m => m.Update(deviceDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetOrdersByDeviceId_GuidSent_OrderDtoListReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeDeviceDtoList()[0].Orders;
        var deviceId = new Guid("dac7436b-5afd-4038-9ade-0581f7bb4712");
        var mock = TestsData.GetFakeDeviceDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Devices)
            .Returns(mock.Object);
        var sut = new DevicesRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetOrdersByDeviceId(deviceId);

        //assert
        Assert.NotNull(actual);
        actual.Should().BeEquivalentTo(expected);
    }
}
