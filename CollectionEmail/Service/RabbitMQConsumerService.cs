using CollectionApplication.Dtos;
using CollectionApplication.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CollectionEmail.Service;

public class RabbitMQConsumerService: BackgroundService
{
    private readonly ConnectionFactory _factory;
    private const string QueueName = "trigger";
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQConsumerService(IConfiguration configuration,
                                   IServiceProvider serviceProvider)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration.GetValue<string>("RabbitMQ:HostName") ?? "localhost",
            AutomaticRecoveryEnabled = true
        };

        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                Console.WriteLine($"[x] Message: {message}");

                var user = JsonSerializer.Deserialize<UserDto>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var processEvent = scope.ServiceProvider.GetRequiredService<IProcessEvent>();

                    ArgumentNullException.ThrowIfNull(user);

                    await emailService.SendEmail(user);
                }

                Console.WriteLine($"[x] Usuário recebido: {user?.Name}");

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Erro ao processar mensagem: {ex.Message}");
            }
        };

        await _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel is not null) await _channel.CloseAsync();
        if (_connection is not null) await _connection.CloseAsync();
        await base.StopAsync(cancellationToken);
    }
}
