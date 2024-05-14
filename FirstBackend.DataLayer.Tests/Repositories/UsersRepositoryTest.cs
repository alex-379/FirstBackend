using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace FirstBackend.DataLayer.Tests.Repositories;

public class UsersRepositoryTest
{
    private readonly Mock<MainerLxContext> _mainerLxContextMock;

    public UsersRepositoryTest()
    {
        _mainerLxContextMock = new Mock<MainerLxContext>();
    }

    [Fact]
    public void GetUsers_Called_ReturnsUserDtoList()
    {
        //arrange
        var expected = 2;
        var mainerLxContextMock = new Mock<MainerLxContext>();
        mainerLxContextMock.Setup(x => x.Users)
            .ReturnsDbSet(TestsData.GetFakeUserDtoList());

        //act
        var usersRepository = new UsersRepository(mainerLxContextMock.Object);
        var actual = usersRepository.GetUsers();

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public void GetUserById_Called_ReturnsUserDto()
    {
        //arrange
        var expected = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566");
        var mock = TestsData.GetFakeUserDtoList().BuildMock().BuildMockDbSet();
        mock.Setup(x => x.Find(expected))
            .Returns(TestsData.GetFakeUserDtoList().Find(e => e.Id == expected));
        var mainerLxContextMock = new Mock<MainerLxContext>();
        mainerLxContextMock.Setup(x => x.Users)
            .Returns(mock.Object);

        //act
        var usersRepository = new UsersRepository(mainerLxContextMock.Object);
        var actual = usersRepository.GetUserById(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.Id);
    }
}
