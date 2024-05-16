using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Repositories;
using MockQueryable.Moq;
using Moq;

namespace FirstBackend.DataLayer.Tests.Repositories;

public class SaltsRepositoryTest
{
    private readonly Mock<SaltLxContext> _saltLxContextMock;

    public SaltsRepositoryTest()
    {
        _saltLxContextMock = new Mock<SaltLxContext>();
    }

    [Fact]
    public void AddSalt_SaltDtoSent_NoErrorsReceieved()
    {
        //arrange
        var expected = 1;
        var salts = new List<SaltDto>();
        var saltDto = TestsData.GetFakeSaltDto();
        var mock = salts.BuildMock().BuildMockDbSet();
        _saltLxContextMock.Setup(x => x.Salts.Add(saltDto))
            .Returns(mock.Object.Add(saltDto))
            .Callback<SaltDto>(salts.Add);
        var sut = new SaltsRepository(_saltLxContextMock.Object);

        //act
        sut.AddSalt(saltDto);

        //assert
        Assert.Equal(expected, salts.Count);
        mock.Verify(m => m.Add(saltDto), Times.Once());
        _saltLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetSaltByUserId_GuidSent_SaltDtoReceieved()
    {
        //arrange
        var expected = new Guid("57fdee39-1522-4a25-b922-b81cf9990123");
        var mock = TestsData.GetFakeSaltDtoList().BuildMock().BuildMockDbSet();
        _saltLxContextMock.Setup(x => x.Salts)
            .Returns(mock.Object);
        var sut = new SaltsRepository(_saltLxContextMock.Object);

        //act
        var actual = sut.GetSaltByUserId(expected);

        //assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.UserId);
    }

    [Fact]
    public void UpdateSalt_SaltDtoSent_NoErrorsReceieved()
    {
        //arrange
        var saltDto = TestsData.GetFakeSaltDtoList()[0];
        var mock = Enumerable.Empty<SaltDto>().BuildMock().BuildMockDbSet();
        _saltLxContextMock.Setup(x => x.Salts.Update(saltDto))
            .Returns(mock.Object.Update(saltDto));
        var sut = new SaltsRepository(_saltLxContextMock.Object);

        //act
        sut.UpdateSalt(saltDto);

        //assert
        mock.Verify(m => m.Update(saltDto), Times.Once());
        _saltLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void DeleteSalt_SaltDtoSent_NoErrorsReceieved()
    {
        //arrange
        var expected = 2;
        var salts = TestsData.GetFakeSaltDtoList();
        var deletedSalts = new List<SaltDto>();
        var saltDto = TestsData.GetFakeSaltDtoList()[0];
        var mock = salts.BuildMock().BuildMockDbSet();
        _saltLxContextMock.Setup(x => x.Salts.Remove(saltDto))
            .Returns(mock.Object.Remove(saltDto))
            .Callback<SaltDto>(deletedSalts.Add);
        var sut = new SaltsRepository(_saltLxContextMock.Object);

        //act
        sut.DeleteSalt(saltDto);
        salts.Remove(salts.FirstOrDefault(s => s.Salt == deletedSalts[0].Salt));

        //assert
        Assert.Equal(expected, salts.Count);
        mock.Verify(m => m.Remove(saltDto), Times.Once());
        _saltLxContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}
