using CollectionApplication.Interfaces;
using CollectionInfrastructure.Service;
using CollectionOutbox.Service;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<OutboxWorker>();

builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

builder.Services.AddSingleton<IMongoClient>(sp =>
{   
    var connectionString = builder.Configuration.GetConnectionString("AuthConnection");
    return new MongoClient(connectionString);
});

var host = builder.Build();

host.Run();
