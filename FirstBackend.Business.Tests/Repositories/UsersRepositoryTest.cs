using AutoMapper;
using FirstBackend.Business.Configuration;
using FirstBackend.Business.Services;
using FirstBackend.Business.Tests.Fixture;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace FirstBackend.Business.Tests.Repositories;

public class UsersRepositoryTest(DbContextFixture fixture) : IClassFixture<DbContextFixture>
{
    private readonly MainerLxContext _contextMainer = fixture.ContextMainer;
    private readonly SaltLxContext _contextSalt = fixture.ContextSalt;

    [Fact]
    public void GetEmployees_WhenCalled_ReturnsEmployeeListAsync()
    {
        //arrange
        var mainerLxContextMock = new Mock<MainerLxContext>();
        mainerLxContextMock.Setup(x => x.Users)
            .ReturnsDbSet(TestDataHelper.GetFakeUserDtoList());

        //act
        EmployeesController employeesController = new(employeeContextMock.Object);
        var employees = (employeesController.GetEmployees()).Value;

        //assert
        Assert.NotNull(employees);
        Assert.Equal(2, employees.Count());
    }
}
