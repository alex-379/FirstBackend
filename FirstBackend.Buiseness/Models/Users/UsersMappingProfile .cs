using AutoMapper;
using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.Buiseness.Models.Users.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Buiseness.Models.Users;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<CreateUserRequest, UserDto>();

        CreateMap<UserDto, UserResponse>();
        CreateMap<UserDto, UserFullResponse>();
    }
}
