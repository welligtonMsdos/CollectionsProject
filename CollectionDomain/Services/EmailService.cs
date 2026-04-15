using CollectionDomain.Dtos.Users;
using CollectionDomain.Interfaces.Services;

namespace CollectionDomain.Services;

public class EmailService : IEmailService
{
    public async Task<bool> SendEmail(UserDto userDto)
    {
        try
        {
            // Enviar email

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
