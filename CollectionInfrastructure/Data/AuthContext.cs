using CollectionDomain.Entities;
using MongoDB.Driver;

namespace CollectionInfrastructure.Data;

public class AuthContext
{
    private readonly IMongoDatabase _database;

    public IMongoCollection<User> Users => _database.GetCollection<User>("User");

    public IMongoCollection<OutboxMessage> OutboxMessages => _database.GetCollection<OutboxMessage>("OutboxMessage");

    public AuthContext(IMongoClient client)
    {   
        _database = client.GetDatabase("TomAuth");
    }
}
