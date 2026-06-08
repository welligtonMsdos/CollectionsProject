using Npgsql;
using System.Data;

namespace Collection10Api.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly string _connectionString;

    protected BaseRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("CollectionConnection")
            ?? throw new ArgumentNullException("Connection string 'CollectionConnection' is missing.");
    }

    protected IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
