using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface IRabbitMQProducer
{
    Task<bool> AddUserDtoAsync(UserDto userDto);
}
