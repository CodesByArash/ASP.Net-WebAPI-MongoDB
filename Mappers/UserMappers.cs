using AuthService.Dtos.UserDtos;
using AuthService.Entities;
using AuthService.Utils;

namespace AuthService.Mappers;

public static class UserMappers
{
    public static User ToUserModel(this RegisterDto dto)
    {
        return new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = Helpers.HashPassword(dto.Password),
        };
    }
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}