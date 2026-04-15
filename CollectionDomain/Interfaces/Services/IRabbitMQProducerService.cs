using CollectionDomain.Models.Users;

namespace CollectionDomain.Interfaces.Services;

public interface IRabbitMQProducerService
{
    public string HostName { get; set; }
    Task<bool> AddUserDtoAsync(User user);
}
