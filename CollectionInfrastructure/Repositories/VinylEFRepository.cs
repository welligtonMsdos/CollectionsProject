using CollectionDomain.Entities;
using CollectionDomain.Interfaces;
using CollectionInfrastructure.Data;

namespace CollectionInfrastructure.Repositories;

public class VinylEFRepository : IVinylEFRepository
{
    private readonly CollectionContext _context;

    public VinylEFRepository(CollectionContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(Vinyl obj)
    {
        _context.vinyls.Remove(obj);

        var deleted = await _context.SaveChangesAsync();

        return deleted > 0;
    }

    public async Task<Vinyl> PostAsync(Vinyl obj)
    {
        await _context.vinyls.AddAsync(obj);

        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<Vinyl> PutAsync(Vinyl obj)
    {
        _context.vinyls.Update(obj);

        await _context.SaveChangesAsync();

        return obj;
    }
}
