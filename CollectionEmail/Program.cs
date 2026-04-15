using CollectionDomain.Interfaces.Services;
using CollectionDomain.Services;
using CollectionEmail.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<RabbitMQConsumerService>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IProcessEvent, ProcessEvent>();

var host = builder.Build();
host.Run();
