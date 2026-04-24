using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CollectionInfrastructure.Service;

public class ProcessEvent : IProcessEvent
{
    private readonly IServiceScopeFactory _service;

    public ProcessEvent(IServiceScopeFactory service)
    {
        _service = service;
    }

    public async Task<bool> Process(string message)
    {
        try
        {
            using var scope = _service.CreateScope();

            var emailRepository = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var userDto = JsonSerializer.Deserialize<UserDto>(message);

            ArgumentNullException.ThrowIfNull(userDto);

            await emailRepository.SendEmail(userDto);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
}
