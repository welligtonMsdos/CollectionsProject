using CollectionDomain.Interfaces.Services;
using CollectionDomain.Models.Users;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CollectionDomain.Services;

public class RabbitMQProducerService : IRabbitMQProducerService
{
    private readonly ConnectionFactory _factory;
    private const string QueueName = "trigger";

    public required string HostName { get; set ; }

    public RabbitMQProducerService()
    {
        _factory = new ConnectionFactory
        {
            HostName = HostName ?? "localhost",
            AutomaticRecoveryEnabled = true
        };
    }

    public async Task<bool> AddUserDtoAsync(User user)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(user);
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
