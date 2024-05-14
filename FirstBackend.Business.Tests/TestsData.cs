using FirstBackend.Core.Dtos;

namespace FirstBackend.Business.Tests;

public static class TestsData
{
    public static IEnumerable<UserDto> GetFakeUserDtoEnumerable() =>
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
