using FirstBackend.API.Controllers;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FirstBackend.API.Tests.Controllers;

public class UsersControllerTest
{
    private readonly Mock<IUsersService> _usersServiceMock;
    private readonly Mock<IDevicesService> _devicesServiceMock;
    private readonly Mock<IOrdersService> _ordersServiceMock;

    public UsersControllerTest()
    {
        _usersServiceMock = new Mock<IUsersService>();
        _devicesServiceMock = new Mock<IDevicesService>();
        _ordersServiceMock = new Mock<IOrdersService>();
    }

    [Fact]
    public void GetUsers_Called_OkResultReceieved()
    {
        //arrange
        _usersServiceMock.Setup(x => x.GetUsers()).Returns([]);
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.GetUsers();

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _usersServiceMock.Verify(m => m.GetUsers(), Times.Once);
    }

    [Fact]
    public void GetUserById_GuidSent_OkResultReceieved()
    {
        //arrange
        var userId = new Guid();
        _usersServiceMock.Setup(x => x.GetUserById(userId)).Returns(new UserFullResponse());
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.GetUserById(userId);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _usersServiceMock.Verify(m => m.GetUserById(userId), Times.Once);
    }

    [Fact]
    public void GetDevicesByUserId_GuidSent_OkResultReceieved()
    {
        //arrange
        var userId = new Guid();
        _devicesServiceMock.Setup(x => x.GetDevicesByUserId(userId)).Returns([]);
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.GetDevicesByUserId(userId);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _devicesServiceMock.Verify(m => m.GetDevicesByUserId(userId), Times.Once);
    }

    [Fact]
    public void GetOrdersByUserId_GuidSent_OkResultReceieved()
    {
        //arrange
        var userId = new Guid();
        _ordersServiceMock.Setup(x => x.GetOrdersByUserId(userId)).Returns([]);
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.GetOrdersByUserId(userId);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _ordersServiceMock.Verify(m => m.GetOrdersByUserId(userId), Times.Once);
    }

    [Fact]
    public void CreateUser_CreateUserRequestSent_CreatedResultReceieved()
    {
        //arrange
        var createUserRequest = new CreateUserRequest();
        _usersServiceMock.Setup(x => x.AddUser(createUserRequest)).Returns(new Guid());
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.CreateUser(createUserRequest);

        //assert
        actual.Result.Should().BeOfType<CreatedResult>();
        _usersServiceMock.Verify(m => m.AddUser(createUserRequest), Times.Once);
    }

    [Fact]
    public void Login_LoginUserRequestSent_OkResultReceieved()
    {
        //arrange
        var loginUserRequest = new LoginUserRequest();
        _usersServiceMock.Setup(x => x.LoginUser(loginUserRequest)).Returns(new AuthenticatedResponse());
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.Login(loginUserRequest);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _usersServiceMock.Verify(m => m.LoginUser(loginUserRequest), Times.Once);
    }

    [Fact]
    public void UpdateUserData_GuidAndUpdateUserDataRequestSent_NoContentResultReceieved()
    {
        //arrange
        var userId = new Guid();
        var updateUserDataRequest = new UpdateUserDataRequest();
        _usersServiceMock.Setup(x => x.UpdateUser(userId, updateUserDataRequest));
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.UpdateUserData(userId, updateUserDataRequest);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _usersServiceMock.Verify(m => m.UpdateUser(userId, updateUserDataRequest), Times.Once);
    }


    [Fact]
    public void DeleteUserById_GuidSent_NoContentResultReceieved()
    {
        //arrange
        var userId = new Guid();
        _usersServiceMock.Setup(x => x.DeleteUserById(userId));
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.DeleteUserById(userId);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _usersServiceMock.Verify(m => m.DeleteUserById(userId), Times.Once);
    }

    [Fact]
    public void UpdateUserPassword_GuidAndUpdateUserDataRequestSent_NoContentResultReceieved()
    {
        //arrange
        var userId = new Guid();
        var updateUserPasswordRequest = new UpdateUserPasswordRequest();
        _usersServiceMock.Setup(x => x.UpdateUserPassword(userId, updateUserPasswordRequest));
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.UpdateUserPassword(userId, updateUserPasswordRequest);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _usersServiceMock.Verify(m => m.UpdateUserPassword(userId, updateUserPasswordRequest), Times.Once);
    }

    [Fact]
    public void UpdateUserMail_GuidAndUpdateUserMailRequestSent_NoContentResultReceieved()
    {
        //arrange
        var userId = new Guid();
        var updateUserMailRequest = new UpdateUserMailRequest();
        _usersServiceMock.Setup(x => x.UpdateUserMail(userId, updateUserMailRequest));
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.UpdateUserMail(userId, updateUserMailRequest);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _usersServiceMock.Verify(m => m.UpdateUserMail(userId, updateUserMailRequest), Times.Once);
    }

    [Fact]
    public void UpdateUserRole_GuidAndUpdateUserRoleRequestSent_NoContentResultReceieved()
    {
        //arrange
        var userId = new Guid();
        var updateUserRoleRequest = new UpdateUserRoleRequest();
        _usersServiceMock.Setup(x => x.UpdateUserRole(userId, updateUserRoleRequest));
        var sut = new UsersController(_usersServiceMock.Object, _devicesServiceMock.Object, _ordersServiceMock.Object);

        //act
        var actual = sut.UpdateUserRole(userId, updateUserRoleRequest);

        //assert
        actual.Should().BeOfType<NoContentResult>();
        _usersServiceMock.Verify(m => m.UpdateUserRole(userId, updateUserRoleRequest), Times.Once);
    }
}
