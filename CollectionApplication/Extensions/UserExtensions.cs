using CollectionApplication.Dtos;
using CollectionDomain.Entities;

namespace CollectionApplication.Extensions;

public static class UserExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDto(user._id,
                           user.Name,
                           user.Email);
    }

    public static UserLoginDto ToLoginDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        return new UserLoginDto(user.Email,
                                user.Password);
    }

    public static UserDataLoginDto ToDataLoginDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var userDto = new UserDto(user._id,
                                  user.Name,
                                  user.Email);

        return new UserDataLoginDto(userDto, user.Password);
    }

    public static User ToEntity(this UserCreateDto userCreateDto)
    {
        return new User
        {
            Name = userCreateDto.Name,
            Email = userCreateDto.Email,
            Password = userCreateDto.Password
        };
    }

    public static void UpdateEntity(this User user, UserUpdateDto userUpdateDto)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(userUpdateDto);
        user.Name = userUpdateDto.Name;
        user.Email = userUpdateDto.Email;
    }
}
