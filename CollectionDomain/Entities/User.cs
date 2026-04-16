using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CollectionDomain.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime LastAccess { get; set; }
    public bool Active { get; set; }
}
