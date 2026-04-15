using CollectionDomain.Interfaces.Services;
using CollectionDomain.Services;
using CollectionOutbox.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<OutboxWorker>();

builder.Services.AddScoped<IRabbitMQProducerService, RabbitMQProducerService>();

var host = builder.Build();
host.Run();
