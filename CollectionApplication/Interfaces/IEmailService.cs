using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmail(UserDto userDto);
}
