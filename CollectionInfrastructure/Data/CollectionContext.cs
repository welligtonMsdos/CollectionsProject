using CollectionDomain.Entities;
using CollectionInfrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CollectionInfrastructure.Data;

public class CollectionContext: DbContext
{
    public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
    {
    }

    public DbSet<Vinyl> vinyls { get; set; }
    public DbSet<Concert> concerts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VinylMap());
        modelBuilder.ApplyConfiguration(new ConcertMap());
    }
}
