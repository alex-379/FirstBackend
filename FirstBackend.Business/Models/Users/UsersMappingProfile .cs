using AutoMapper;
using FirstBackend.Business.Models.Users.Requests;
using FirstBackend.Business.Models.Users.Responses;
using FirstBackend.Core.Dtos;

namespace FirstBackend.Business.Models.Users;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<CreateUserRequest, UserDto>();
        CreateMap<LoginUserRequest, UserDto>();

        CreateMap<UserDto, UserResponse>();
        CreateMap<UserDto, UserFullResponse>();
    }
}
