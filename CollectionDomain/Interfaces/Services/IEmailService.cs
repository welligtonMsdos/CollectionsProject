using CollectionDomain.Dtos.Users;

namespace CollectionDomain.Interfaces.Services;

public interface IEmailService
{
    Task<bool> SendEmail(UserDto userDto);
}
