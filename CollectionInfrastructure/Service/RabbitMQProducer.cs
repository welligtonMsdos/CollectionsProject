using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CollectionInfrastructure.Service;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly ConnectionFactory _factory;
    private const string QueueName = "trigger";

    public RabbitMQProducer(IConfiguration configuration)
    {
        var host = configuration["RabbitMQ:Host"] ?? "localhost";

        _factory = new ConnectionFactory
        {
            HostName = host,
            AutomaticRecoveryEnabled = true
        };
    }

    public async Task<bool> AddUserDtoAsync(UserDto userDto)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(userDto);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: QueueName,
            mandatory: true,
            basicProperties: new BasicProperties { DeliveryMode = DeliveryModes.Persistent },
            body: body);

        return true;
    }
}
