using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Devices;
using FirstBackend.Business.Models.Orders;
using FirstBackend.Business.Models.Users;
using FirstBackend.Business.Services;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exñeptions;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace FirstBackend.Business.Tests.Services;

public class UsersServiceTest
{
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mock<ISaltsRepository> _saltsRepositoryMock;
    private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
    private readonly Mock<ITransactionsRepository<MainerLxContext>> _transactionMainerLxRepository;
    private readonly Mock<ITransactionsRepository<SaltLxContext>> _transactionSaltLxRepository;
    private readonly SecretSettings _secret;
    private readonly JwtToken _jwt;
    private readonly IPasswordsService _passwordsService;
    private readonly ITokensService _tokensService;
    private readonly Mapper _mapper;

    public UsersServiceTest()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _saltsRepositoryMock = new Mock<ISaltsRepository>();
        _ordersRepositoryMock = new Mock<IOrdersRepository>();
        _transactionMainerLxRepository = new Mock<ITransactionsRepository<MainerLxContext>>();
        _transactionSaltLxRepository = new Mock<ITransactionsRepository<SaltLxContext>>();
        _secret = new SecretSettings();
        _jwt = new JwtToken();
        _passwordsService = new PasswordsService(_secret);
        _tokensService = new TokensService(_secret, _jwt, _usersRepositoryMock.Object);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UsersMappingProfile());
            cfg.AddProfile(new OrdersMappingProfile());
            cfg.AddProfile(new DevicesMappingProfile());
        });

        _mapper = new Mapper(config);
    }

    [Fact]
    public void AddUser_CreateUserRequestSent_GuidReceieved()
    {
        //arrange
        var createUserRequest = TestsData.GetFakeCreateUserRequest();
        var expectedGuid = Guid.NewGuid();
        _transactionMainerLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _transactionSaltLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        _transactionMainerLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        _transactionSaltLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        var actual = sut.AddUser(createUserRequest);

        //assert
        Assert.Equal(expectedGuid, actual);
        _transactionMainerLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _usersRepositoryMock.Verify(m => m.AddUser(It.IsAny<UserDto>()), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
    }

    [Fact]
    public void AddUser_CreateUserRequestWithDuplicateMailSent_ConflictErrorReceieved()
    {
        //arrange
        var createUserRequestWithDuplicateMail = TestsData.GetFakeCreateUserRequest();
        _usersRepositoryMock.Setup(x => x.GetUserByMail(createUserRequestWithDuplicateMail.Mail)).Returns(new UserDto());
        _usersRepositoryMock.Setup(x => x.AddUser(It.IsAny<UserDto>())).Returns(Guid.NewGuid());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.AddUser(createUserRequestWithDuplicateMail);

        //assert
        act.Should().Throw<ConflictException>()
            .WithMessage(UsersServiceExceptions.ConflictException);
        _usersRepositoryMock.Verify(m => m.AddUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void LoginUser_LoginUserRequestIncorrectMailSent_UserUnauthenticatedErrorReceieved()
    {
        //arrange
        var loginUserRequestIncorrectMail = TestsData.GetFakeLoginUserRequest();
        _usersRepositoryMock.Setup(x => x.GetUserByMail(loginUserRequestIncorrectMail.Mail)).Returns((UserDto)null);
        var saltDto = TestsData.GetFakeSaltDto();
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.LoginUser(loginUserRequestIncorrectMail);

        //assert
        act.Should().Throw<UnauthenticatedException>();
        _usersRepositoryMock.Verify(m => m.GetUserByMail(loginUserRequestIncorrectMail.Mail), Times.Once);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(saltDto.UserId), Times.Never);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void LoginUser_LoginUserRequestIncorrectPasswordSent_UserUnauthenticatedErrorReceieved()
    {
        //arrange
        var loginUserRequestIncorrectPassword = TestsData.GetFakeLoginUserRequest();
        var userDto = TestsData.GetFakeUserDto();
        _usersRepositoryMock.Setup(x => x.GetUserByMail(loginUserRequestIncorrectPassword.Mail)).Returns(userDto);
        var saltDto = TestsData.GetFakeSaltDto();
        _saltsRepositoryMock.Setup(x => x.GetSaltByUserId(userDto.Id)).Returns(saltDto);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.LoginUser(loginUserRequestIncorrectPassword);

        //assert
        act.Should().Throw<UnauthenticatedException>();
        _usersRepositoryMock.Verify(m => m.GetUserByMail(loginUserRequestIncorrectPassword.Mail), Times.Once);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(saltDto.UserId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void GetUsers_Calles_ListUserResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeListUserResponse();
        var expectedUsers = TestsData.GetFakeListUserDto();
        _usersRepositoryMock.Setup(x => x.GetUsers()).Returns(expectedUsers);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        var actual = sut.GetUsers();

        //assert
        actual.Should().BeEquivalentTo(expected);
        _usersRepositoryMock.Verify(m => m.GetUsers(), Times.Once);
    }

    [Fact]
    public void GetUserById_GuidSent_UserFullResponseReceieved()
    {
        //arrange
        var expected = TestsData.GetFakeUserFullResponse();
        var expectedUser = TestsData.GetFakeUserDto();
        var userId = Guid.NewGuid();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(expectedUser);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        var actual = sut.GetUserById(userId);

        //assert
        actual.Should().BeEquivalentTo(expected);
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
    }

    [Fact]
    public void GetUserByIdNoUser_EmptyGuidSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.GetUserById(userId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
    }

    [Fact]
    public void UpdateUser_GuidAndUpdateUserDataRequestSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var updateUserDataRequest = TestsData.GetFakeUpdateUserDataRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        sut.UpdateUser(userId, updateUserDataRequest);

        //assert
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Once);
    }

    [Fact]
    public void UpdateUserNoUser_EmptyGuidAndUpdateUserDataRequestSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        var updateUserDataRequest = TestsData.GetFakeUpdateUserDataRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.UpdateUser(userId, updateUserDataRequest);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void UpdateUserPassword_GuidAndUpdateUserPasswordRequestSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var updateUserPasswordRequest = TestsData.GetFakeUpdateUserPasswordRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
        _transactionMainerLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _transactionSaltLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _saltsRepositoryMock.Setup(x => x.GetSaltByUserId(userId)).Returns(new SaltDto());
        _transactionMainerLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        _transactionSaltLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        sut.UpdateUserPassword(userId, updateUserPasswordRequest);

        //assert
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Once);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(userId), Times.Once);
        _saltsRepositoryMock.Verify(m => m.UpdateSalt(It.IsAny<SaltDto>()), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
    }

    [Fact]
    public void UpdateUserPasswordNoUser_EmptyGuidAndUpdateUserPasswordRequestSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        var updateUserPasswordRequest = TestsData.GetFakeUpdateUserPasswordRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        _transactionMainerLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _transactionSaltLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _saltsRepositoryMock.Setup(x => x.GetSaltByUserId(userId)).Returns(new SaltDto());
        _transactionMainerLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        _transactionSaltLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.UpdateUserPassword(userId, updateUserPasswordRequest);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.BeginTransaction(), Times.Never);
        _transactionSaltLxRepository.Verify(m => m.BeginTransaction(), Times.Never);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(userId), Times.Never);
        _saltsRepositoryMock.Verify(m => m.UpdateSalt(It.IsAny<SaltDto>()), Times.Never);
        _transactionMainerLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Never);
        _transactionSaltLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Never);
    }

    [Fact]
    public void UpdateUserMail_GuidAndUpdateUserMailRequestSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var updateUserMailRequest = TestsData.GetFakeUpdateUserMailRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        sut.UpdateUserMail(userId, updateUserMailRequest);

        //assert
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Once);
    }

    [Fact]
    public void UpdateUserMailNoUser_EmptyGuidAndUpdateUserMailRequestSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        var updateUserMailRequest = TestsData.GetFakeUpdateUserMailRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.UpdateUserMail(userId, updateUserMailRequest);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void UpdateUserMailDuplicateMail_GuidAndUpdateUserMailRequestSent_ConflictErrorReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var updateUserMailRequest = TestsData.GetFakeUpdateUserMailRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
        _usersRepositoryMock.Setup(x => x.GetUserByMail(updateUserMailRequest.Mail)).Returns(new UserDto());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.UpdateUserMail(userId, updateUserMailRequest);

        //assert
        act.Should().Throw<ConflictException>()
            .WithMessage(UsersServiceExceptions.ConflictException);
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.GetUserByMail(updateUserMailRequest.Mail), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void UpdateUserRole_GuidAndUpdateUserRoleRequestSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var updateUserRoleRequest = TestsData.GetFakeUpdateUserRoleRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(new UserDto());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        sut.UpdateUserRole(userId, updateUserRoleRequest);

        //assert
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Once);
    }

    [Fact]
    public void UpdateUserRoleNoUser_EmptyGuidAndUpdateUserRoleRequestSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        var updateUserRoleRequest = TestsData.GetFakeUpdateUserRoleRequest();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.UpdateUserRole(userId, updateUserRoleRequest);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void DeleteUserById_GuidSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        var userDto = TestsData.GetFakeUserDto();
        var saltDto = TestsData.GetFakeSaltDto();
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(userDto);
        _transactionMainerLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _transactionSaltLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _saltsRepositoryMock.Setup(x => x.GetSaltByUserId(userDto.Id)).Returns(saltDto);
        _transactionMainerLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        _transactionSaltLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        sut.DeleteUserById(userId);

        //assert
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.BeginTransaction(), Times.Once);
        _usersRepositoryMock.Verify(m => m.UpdateUser(userDto), Times.Once);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(userDto.Id), Times.Once);
        _saltsRepositoryMock.Verify(m => m.DeleteSalt(saltDto), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
        _transactionSaltLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Once);
    }

    [Fact]
    public void DeleteUserById_EmptyGuidSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        _usersRepositoryMock.Setup(x => x.GetUserById(userId)).Returns((UserDto)null);
        _transactionMainerLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _transactionSaltLxRepository.Setup(x => x.BeginTransaction()).Returns(It.IsAny<IDbContextTransaction>());
        _saltsRepositoryMock.Setup(x => x.GetSaltByUserId(It.IsAny<Guid>())).Returns(new SaltDto());
        _transactionMainerLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        _transactionSaltLxRepository.Setup(x => x.CommitTransaction(It.IsAny<IDbContextTransaction>()));
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.DeleteUserById(userId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(m => m.GetUserById(userId), Times.Once);
        _transactionMainerLxRepository.Verify(m => m.BeginTransaction(), Times.Never);
        _transactionSaltLxRepository.Verify(m => m.BeginTransaction(), Times.Never);
        _usersRepositoryMock.Verify(m => m.UpdateUser(It.IsAny<UserDto>()), Times.Never);
        _saltsRepositoryMock.Verify(m => m.GetSaltByUserId(It.IsAny<Guid>()), Times.Never);
        _saltsRepositoryMock.Verify(m => m.DeleteSalt(It.IsAny<SaltDto>()), Times.Never);
        _transactionMainerLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Never);
        _transactionSaltLxRepository.Verify(m => m.CommitTransaction(It.IsAny<IDbContextTransaction>()), Times.Never);
    }

    [Fact]
    public void GetUserIdByOrderId_GuidOrderSent_GuidUserReceieved()
    {
        //arrange
        var expectedGuid = new Guid("865179f5-1adb-4788-9fed-b9a57ce9ab65");
        var orderId = Guid.NewGuid();
        var orderDto = TestsData.GetFakeOrderDto();
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns(orderDto);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        var actual = sut.GetUserIdByOrderId(orderId);

        //assert
        actual.Should().Be(expectedGuid);
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
    }

    [Fact]
    public void GetUserIdByOrderIdNoOrder_EmptyGuidOrderSent_OrderNotFoundErrorReceieved()
    {
        //arrange
        var orderId = Guid.Empty;
        _ordersRepositoryMock.Setup(x => x.GetOrderById(orderId)).Returns((OrderDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _ordersRepositoryMock.Object,
            _transactionMainerLxRepository.Object, _transactionSaltLxRepository.Object,
            _passwordsService, _tokensService, _mapper, _jwt);

        //act
        Action act = () => sut.GetUserIdByOrderId(orderId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(OrdersServiceExceptions.NotFoundException, orderId));
        _ordersRepositoryMock.Verify(m => m.GetOrderById(orderId), Times.Once);
    }
}
