using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Users;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Business.Services;
using FirstBackend.Business.Tests.Fixture;
using FirstBackend.Core.Constants.Exceptions.Business;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Exñeptions;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using FluentAssertions;
using Moq;

namespace FirstBackend.Business.Tests.Services;

public class UsersServiceTest : IClassFixture<DbContextFixture>
{
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mock<ISaltsRepository> _saltsRepositoryMock;
    private readonly SecretSettings _secret;
    private readonly JwtToken _jwt;
    private readonly IPasswordsService _passwordsService;
    private readonly ITokensService _tokensService;
    private readonly Mapper _mapper;
    private readonly SaltLxContext _contextSalt;
    private readonly MainerLxContext _contextMainer;

    public UsersServiceTest(DbContextFixture fixture)
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _saltsRepositoryMock = new Mock<ISaltsRepository>();
        _secret = new SecretSettings();
        _jwt = new JwtToken();
        _passwordsService = new PasswordsService(_secret);
        _tokensService = new TokensService(_secret, _jwt, _usersRepositoryMock.Object);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UsersMappingProfile());
        });

        _mapper = new Mapper(config);
        _contextSalt = fixture.ContextSalt;
        _contextMainer = fixture.ContextMainer;
    }

    [Fact]
    public void AddUser_ValidCreateUserRequestSent_GuidReceieved()
    {
        //arrange
        var validCreateUserRequest = new CreateUserRequest()
        {
            Name = "Test",
            Mail = "test@test",
            Password = "password"
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        var actual = sut.AddUser(validCreateUserRequest);

        //assert
        Assert.Equal(expectedGuid, actual);
    }

    [Fact]
    public void AddUser_CreateUserRequestWithDuplicateMailSent_ConflictErrorReceieved()
    {
        //arrange
        var mail = "test@test";
        var CreateUserRequestWithDuplicateMail = new CreateUserRequest()
        {
            Name = "Test",
            Mail = mail,
            Password = "password"
        };
        var expectedUser = new UserDto()
        {
            Mail = mail,
        };
        _usersRepositoryMock.Setup(r => r.GetUserByMail(mail)).Returns(expectedUser);
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        Action act = () => sut.AddUser(CreateUserRequestWithDuplicateMail);

        //assert
        act.Should().Throw<ConflictException>()
            .WithMessage(UsersServiceExceptions.ConflictException);
        _usersRepositoryMock.Verify(r => r.AddUser(It.IsAny<UserDto>()), Times.Never);
    }

    [Fact]
    public void GetAllUsers_Calles_UsersReceieved()
    {
        //arrange
        var userMail1 = "test@test";
        var userMail2 = "test2@test";
        var expexted = new List<UserResponse>()
        {
            new()
            {
                Mail = userMail1,
            },
            new()
            {
                Mail = userMail2,
            },
        };
        var expectedUsers = new List<UserDto>()
        {
            new()
            {
                Mail = userMail1,
            },
            new()
            {
                Mail = userMail2,
            },
        };
        _usersRepositoryMock.Setup(r => r.GetAllUsers()).Returns(expectedUsers);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        var actual = sut.GetAllUsers();

        //assert
        actual.Should().BeEquivalentTo(expexted);
    }

    [Fact]
    public void DeleteUserById_ValidGuidSent_NoErrorsReceieved()
    {
        //arrange
        var userId = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.GetUserById(userId)).Returns(new UserDto());
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        sut.DeleteUserById(userId);

        //assert
        //Assert.Equal(expectedGuid, actual);
        _usersRepositoryMock.Verify(r => r.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserDto>()), Times.Once);
    }

    [Fact]
    public void DeleteUserById_EmptyGuidSent_UserNotFoundErrorReceieved()
    {
        //arrange
        var userId = Guid.Empty;
        _usersRepositoryMock.Setup(r => r.GetUserById(userId)).Returns((UserDto)null);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        Action act = () => sut.DeleteUserById(userId);

        //assert
        act.Should().Throw<NotFoundException>()
            .WithMessage(string.Format(UsersServiceExceptions.NotFoundException, userId));
        _usersRepositoryMock.Verify(r => r.GetUserById(userId), Times.Once);
        _usersRepositoryMock.Verify(r => r.UpdateUser(It.IsAny<UserDto>()), Times.Never);
    }
}