using CollectionDomain.Models.Users;

namespace CollectionDomain.Interfaces.Services;

public interface IRabbitMQProducerService
{   
    Task<bool> AddUserDtoAsync(User user);
}
