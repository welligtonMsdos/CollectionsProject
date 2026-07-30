using Collection10Api.Domain.Entities;
using Collection10Api.Domain.Interfaces;
using Collection10Api.Infrastructure.Data;

namespace Collection10Api.Infrastructure.Repositories;

public class VinylEFRepository : IVinylEFRepository
{
    private readonly CollectionContext _context;

    public VinylEFRepository(CollectionContext context)
    {
        _context = context;
    }

    public async Task<Vinyl> CreateVinylAsync(Vinyl obj)
    {
        await _context.vinyls.AddAsync(obj);

        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<Vinyl> UpdateVinylAsync(Vinyl obj)
    {
        _context.vinyls.Update(obj);

        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> DeleteVinylAsync(Vinyl obj)
    {
        _context.vinyls.Remove(obj);

        var deleted = await _context.SaveChangesAsync();

        return deleted > 0;
    }    
}
