using FirstBackend.Core.Dtos;

namespace FirstBackend.Business.Tests.Repositories;

public static class TestDataHelper
{
    public static List<UserDto> GetFakeUserDtoList() =>
        [
        new()
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Mail = "J.D@gmail.com",
            Password = "123-456-7890"
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Mark Luther",
            Mail = "M.L@gmail.com",
            Password = "123-456-7890"
        }
        ];
}
