using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IVinylEFRepository 
{
    Task<Vinyl> CreateVinylAsync(Vinyl obj);
    Task<Vinyl> UpdateVinylAsync(Vinyl obj);
    Task<bool> DeleteVinylAsync(Vinyl obj);
}
