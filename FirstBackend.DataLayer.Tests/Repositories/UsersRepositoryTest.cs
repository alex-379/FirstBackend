using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit.Sdk;

namespace FirstBackend.DataLayer.Tests.Repositories;

public class UsersRepositoryTest
{
    private readonly Mock<MainerLxContext> _mainerLxContextMock;
    private readonly Mock<DbSet<UserDto>> _usersSetMock;

    public UsersRepositoryTest()
    {
        _mainerLxContextMock = new Mock<MainerLxContext>();
        _usersSetMock = new Mock<DbSet<UserDto>>();
    }

    [Fact]
    public void AddUser_UserDtoSent_GuidReceieved()
    {
        //arrange
        var expected = 3;
        var users = TestsData.GetFakeUserDtoList();
        var userDto = TestsData.GetFakeUserDto();
        var mock = users.BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Users.Add(userDto))
            .Returns(mock.Object.Add(userDto))
            .Callback<UserDto>(users.Add);
        var sut = new UsersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.AddUser(userDto);

        //assert
        Assert.Equal(expected, users.Count);
        mock.Verify(m => m.Add(userDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetUsers_Called_UserDtoListReceieved()
    {
        //arrange
        var expected = 2;
        _mainerLxContextMock.Setup(x => x.Users)
            .ReturnsDbSet(TestsData.GetFakeUserDtoList());
        var sut = new UsersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetUsers();

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public void GetUserById_GuidSent_UserDtoReceieved()
    {
        //arrange
        var expected = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566");
        var mock = TestsData.GetFakeUserDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Users)
            .Returns(mock.Object);
        var sut = new UsersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetUserById(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }

    [Fact]
    public void GetUserByMail_MailSent_UserDtoReceieved()
    {
        //arrange
        var mail = "test401@test.ru";
        var expected = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890");
        var mock = TestsData.GetFakeUserDtoList().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Users)
            .Returns(mock.Object);
        var sut = new UsersRepository(_mainerLxContextMock.Object);

        //act
        var actual = sut.GetUserByMail(mail);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }

    [Fact]
    public void UpdateUser_UserDtoSent_NoErrorsReceieved()
    {
        //arrange
        var userDto = TestsData.GetFakeUserDto();
        var mock = Enumerable.Empty<UserDto>().BuildMock().BuildMockDbSet();
        _mainerLxContextMock.Setup(x => x.Users.Update(userDto))
            .Returns(mock.Object.Update(userDto));
        var sut = new UsersRepository(_mainerLxContextMock.Object);

        //act
        sut.UpdateUser(userDto);

        //assert
        mock.Verify(m => m.Update(userDto), Times.Once());
        _mainerLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}