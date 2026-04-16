using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;

namespace CollectionInfrastructure.Service;

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
