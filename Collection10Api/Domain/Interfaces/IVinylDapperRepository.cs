using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IVinylDapperRepository 
{
    Task<IEnumerable<Vinyl>> GetVinylsAsync(string userId);
    Task<IEnumerable<Vinyl>> GetVinylByComboAsync(string userId);
    Task<Vinyl?> GetVinylByGuidAsync(Guid guid);       
}
