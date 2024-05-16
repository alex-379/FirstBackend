using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;

namespace FirstBackend.Business.Tests;

public static class TestsData
{
    public static CreateUserRequest GetFakeCreateUserRequest() =>
        new()
        {
            Name = "TestUser408",
            Mail = "test408@test.ru",
            Password = "passPass010"
        };

    public static UserDto GetFakeUserDto() =>
        new()
        {
            Id = new Guid("865179f5-1adb-4788-9fed-b9a57ce9abf8"),
            Name = "testuser408",
            Mail = "test408@test.ru",
            Password = "6D208A964DBEB743626BAAA8C1048EF31C882764F74F89E368728721CA6A922042C9E4D6D50D69D1A707B45FF680674EFC299BD93AF48DEAC0B5F862EB080728",
            Orders =
            [
                new()
                {
                    Description = "Заказ № 655",
                    Devices =[],
                }
            ]
        };

    public static List<UserResponse> GetFakeListUserResponse() =>
        [
            new()
            {
            Mail = "test1@test",
            },
            new()
            {
            Mail = "test2@test",
            },
        ];

    public static List<UserDto> GetFakeListUserDto() =>
    [
        new()
            {
            Mail = "test1@test",
            },
            new()
            {
            Mail = "test2@test",
            },
        ];

    public static LoginUserRequest GetFakeLoginUserRequest() =>
        new()
        {
            Mail = "test408@test.ru",
            Password = "passPass010"
        };

    public static SaltDto GetFakeSaltDto() =>
        new()
        {
            UserId = new Guid("865179f5-1adb-4788-9fed-b9a57ce9abf8"),
            Salt = "9C0A07523B63A31DFF0FF106003A8113A756679C0D29D72CFCE1BA3B05EB03ADE4A7ED1D32DC47A32BB93F5039274D619A981AD83CFF6676E0EC8D42C7046A7B",
        };

    public static UserFullResponse GetFakeUserFullResponse() =>
        new()
        {
            Id = new Guid("865179f5-1adb-4788-9fed-b9a57ce9abf8"),
            Name = "testuser408",
            Mail = "test408@test.ru",
            Orders =
            [
                new()
                {
                    Description = "Заказ № 655",
                    Devices =[],
                }
            ]
        };

    public static UpdateUserDataRequest GetFakeUpdateUserDataRequest() =>
        new()
        {
            Name = "testuser555",
        };

    public static UpdateUserPasswordRequest GetFakeUpdateUserPasswordRequest() =>
        new()
        {
            Password = "PassPass0012",
        };

    public static UpdateUserMailRequest GetFakeUpdateUserMailRequest() =>
        new()
        {
            Mail = "test508@test.ru",
        };

    public static UpdateUserRoleRequest GetFakeUpdateUserRoleRequest() =>
        new()
        {
            Role = UserRole.Administrator,
        };
}