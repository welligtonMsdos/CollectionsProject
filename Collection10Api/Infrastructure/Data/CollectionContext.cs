using Collection10Api.Domain.Entities;
using Collection10Api.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Collection10Api.Infrastructure.Data;

public class CollectionContext : DbContext
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
