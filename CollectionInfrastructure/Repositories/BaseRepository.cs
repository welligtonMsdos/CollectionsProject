using Npgsql;
using System.Data;

namespace CollectionInfrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly string _connectionString;

    protected BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
