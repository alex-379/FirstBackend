using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Tests;

public static class TestsData
{
    public static List<UserDto> GetFakeUserDtoList() =>
        [
        new()
        {
            Id = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566"),
            Name = "testuser01",
            Mail = "test01@test.ru",
        },
        new()
        {
            Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
            Name = "testuser401",
            Mail = "test401@test.ru",
        }
        ];
}
