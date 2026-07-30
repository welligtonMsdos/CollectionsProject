using Collection10Api.Domain.Entities;
using Collection10Api.Domain.Interfaces;
using Collection10Api.Infrastructure.Data;

namespace Collection10Api.Infrastructure.Repositories;

public class ConcertEFRepository : IConcertEFRepository
{
    private readonly CollectionContext _context;

    public ConcertEFRepository(CollectionContext context)
    {
        _context = context;
    }

    public async Task<Concert> CreateConcertAsync(Concert obj)
    {
        await _context.concerts.AddAsync(obj);

        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<Concert> UpdateConcertAsync(Concert obj)
    {
        _context.concerts.Update(obj);

        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> DeleteConcertAsync(Concert obj)
    {
        _context.concerts.Remove(obj);

        var deleted = await _context.SaveChangesAsync();

        return deleted > 0;
    }     
}
