using CollectionApplication.Interfaces;
using CollectionInfrastructure.Service;
using CollectionOutbox.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<OutboxWorker>();

builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

var host = builder.Build();
host.Run();
