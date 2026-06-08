using Collection10Api.Domain.Entities;
using Collection10Api.Domain.Interfaces;
using Dapper;

namespace Collection10Api.Infrastructure.Repositories;

public class VinylDapperRepository : BaseRepository, IVinylDapperRepository
{
    public VinylDapperRepository(IConfiguration config) : base(config) { }

    public async Task<IEnumerable<Vinyl>> GetAsync(string userId)
    {
        using var connection = CreateConnection();

        var query = @"SELECT ""Guid"",""Artist"",""Album"",""Year"",""Photo"",""Price"",""Active"" 
                      FROM ""Vinyl""
                      WHERE ""Active"" = TRUE AND
                      ""UserId"" = @UserId
                      ORDER BY ""Year""";

        return await connection.QueryAsync<Vinyl>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Vinyl>> GetByComboAsync(string userId)
    {
        using var connection = CreateConnection();

        var query = @"
                        SELECT DISTINCT ON (""Artist"") 
                            ""Guid"", 
                            ""Artist""
                        FROM ""Vinyl""
                        WHERE ""Active"" = TRUE AND 
                              ""UserId"" = @UserId
                        ORDER BY ""Artist"", ""Year"" DESC";

        return await connection.QueryAsync<Vinyl>(query, new { UserId = userId });
    }

    public async Task<Vinyl?> GetByGuidAsync(Guid guid)
    {
        using var connection = CreateConnection();

        var query = @"SELECT ""Guid"",""Artist"",""Album"",""Year"",""Photo"",""Price"",""Active""
                      FROM ""Vinyl"" 
                      WHERE ""Guid"" = @Guid
                      ORDER BY ""Year""";

        return await connection.QueryFirstOrDefaultAsync<Vinyl>(query, new { Guid = guid });
    }
}
