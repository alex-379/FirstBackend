using FirstBackend.Core.Dtos;
using FirstBackend.Core.Enums;

namespace FirstBackend.DataLayer.Tests;

public static class TestsData
{
    public static UserDto GetFakeUserDto() =>
        new()
        {
            Name = "testuser03",
            Mail = "test03@test.ru",
        };

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

    public static SaltDto GetFakeSaltDto() =>
        new()
        {
            UserId = new Guid("06bc5305-00e4-4eff-bce6-d49587d42fc0"),
            Salt = "A546035A15B2F59130B3907FE3CFB3D5641CF49DB1EBC36F2C49994F312E6860994A9AEC87665A0B1043553F20306C63C15A0C5A71822C77269AE1B82CC2A721",
        };

    public static List<SaltDto> GetFakeSaltDtoList() =>
        [
        new()
        {
            UserId = new Guid("06bc5305-00e4-4eff-bce6-d49587d42fc0"),
            Salt = "A546035A15B2F59130B3907FE3CFB3D5641CF49DB1EBC36F2C49994F312E6860994A9AEC87665A0B1043553F20306C63C15A0C5A71822C77269AE1B82CC2A721",
        },
        new()
        {
            UserId = new Guid("57fdee39-1522-4a25-b922-b81cf9990123"),
            Salt = "3DD96C76759319CA011329E7A21CF6DBE8A6572CBE1F1C121D2E652AC3814F3A873FD34C009997D49322C8C5DF6E22BAEE05A9CEE68B3D67EA762CBA07C6F0B9",
        },
        new()
        {
            UserId = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566"),
            Salt = "B477D3715C47C8B3129DBDE32CACB8170FAA140FF1899323B59D0DA62A4288186DAB32FC14656B214360C049172F1F65CEDCEA18CEAF3BC52189D5FEA30BAB4B",
        }
        ];

    public static OrderDto GetFakeOrderDto() =>
        new()
        {
            Description = "Заказ № 555",
            Customer = new()
            {
                Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                Name = "testuser401",
                Mail = "test401@test.ru",
            }
        };

    public static List<OrderDto> GetFakeOrderDtoList() =>
        [
        new()
        {
            Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
            Description = "Заказ № 555",
            Customer = new()
            {
                Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                Name = "testuser401",
                Mail = "test401@test.ru",
            }
        },
        new()
        {
            Id = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566"),
            Description = "Заказ № 888",
            Customer = new()
            {
                Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92894"),
                Name = "testuser403",
                Mail = "test403@test.ru",
            }
        }
        ];


    public static DeviceDto GetFakeDeviceDto() =>
    new()
    {
        Id = new Guid("dac7436b-5afd-4038-9ade-0581f7bb471f"),
        Name = "Dell",
        Type = DeviceType.Laptop,
        Address = "Khabarovsk",
    };

    public static List<DeviceDto> GetFakeDeviceDtoList() =>
        [
        new()
        {
            Id = new Guid("dac7436b-5afd-4038-9ade-0581f7bb4712"),
            Name = "Гравитон",
            Type = DeviceType.Laptop,
            Address = "Saint-Petersburg",
            Orders = [
                new()
                {
                    Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                    Description = "Заказ № 555",
                    Customer = new()
                    {
                        Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                        Name = "testuser401",
                        Mail = "test401@test.ru",
                    }
                },
                new()
                {
                    Id = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566"),
                    Description = "Заказ № 888",
                    Customer = new()
                    {
                        Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92894"),
                        Name = "testuser403",
                        Mail = "test403@test.ru",
                    }
                }
                ],
        },
        new()
        {
            Id = new Guid("dac7436b-5afd-4038-9ade-0581f7bb4716"),
            Name = "Dell",
            Type = DeviceType.PC,
            Address = "Khabarovsk",
            Orders = [
                new()
                {
                    Id = new Guid("4e7918d2-fdcd-4316-97bb-565f8f4a0566"),
                    Description = "Заказ № 888",
                    Customer = new()
                    {
                        Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92894"),
                        Name = "testuser403",
                        Mail = "test403@test.ru",
                    }
                }
                ],
        },
        new()
        {
            Id = new Guid("dac7436b-5afd-4038-9ade-0581f7bb4714"),
            Name = "Radeon",
            Type = DeviceType.VideoCard,
            Address = "Moscow",
            Orders = [
                new()
                {
                    Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                    Description = "Заказ № 555",
                    Customer = new()
                    {
                        Id = new Guid("78fa8b9b-91fa-4e94-9a35-33d356d92890"),
                        Name = "testuser401",
                        Mail = "test401@test.ru",
                    }
                },
                ],
        },
        ];
}