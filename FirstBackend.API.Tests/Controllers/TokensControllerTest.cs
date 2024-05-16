using FirstBackend.API.Controllers;
using FirstBackend.Business.Interfaces;
using FirstBackend.Business.Models.Tokens.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FirstBackend.API.Tests.Controllers;

public class TokensControllerTest
{
    private readonly Mock<ITokensService> _tokenServiceMock;

    public TokensControllerTest()
    {
        _tokenServiceMock = new Mock<ITokensService>();
    }

    [Fact]
    public void Refresh_RefreshTokenRequestSent_OkResultReceieved()
    {
        //arrange
        var refreshTokenRequest = new RefreshTokenRequest();
        _tokenServiceMock.Setup(x => x.Refresh(refreshTokenRequest)).Returns(It.IsAny<AuthenticatedResponse>);
        var sut = new TokensController(_tokenServiceMock.Object);

        //act
        var actual = sut.Refresh(refreshTokenRequest);

        //assert
        actual.Result.Should().BeOfType<OkObjectResult>();
        _tokenServiceMock.Verify(m => m.Refresh(refreshTokenRequest), Times.Once);
    }
}
