using FirstBackend.Core.Constants.Tests;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace FirstBackend.Business.Tests.Repositories;

public class UsersRepositoryTest()
{
    [Fact]
    public void GetUsers_Called_ReturnsUserDtoEnumerable()
    {
        //arrange
        var expexted = TestsDataConstants.NumberUsers;
        var mainerLxContextMock = new Mock<MainerLxContext>();
        mainerLxContextMock.Setup(x => x.Users)
            .ReturnsDbSet(TestsData.GetFakeUserDtoEnumerable());

        //act
        var usersRepository = new UsersRepository(mainerLxContextMock.Object);
        var actual = usersRepository.GetUsers();

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expexted, actual.Count());
    }
}