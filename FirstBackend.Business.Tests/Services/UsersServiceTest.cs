using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Users;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Services;
using FirstBackend.Business.Tests.Fixture;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
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
    public void AddUserTest_CreateUserRequestWithInvalidMailSent_MailErrorReceieved()
    {
        //arange
        var validCreateUserRequest = new CreateUserRequest()
        {
            Name = "Test",
            Mail = "testtest",
            Password = "password"
        };
        var expectedGuid = Guid.NewGuid();
        _usersRepositoryMock.Setup(r => r.AddUser(It.IsAny<UserDto>())).Returns(expectedGuid);
        var sut = new UsersService(_usersRepositoryMock.Object, _saltsRepositoryMock.Object, _passwordsService, _tokensService, _mapper, _jwt, _contextSalt, _contextMainer);

        //act
        var actual = sut.AddUser(validCreateUserRequest);

        //assert
        Assert.Equal(expectedGuid, actual);
        _usersRepositoryMock.Verify(r => r.AddUser(It.IsAny<UserDto>()), Times.Once);
    }
}