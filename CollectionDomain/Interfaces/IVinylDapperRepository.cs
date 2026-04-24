using CollectionDomain.Entities;

namespace CollectionDomain.Interfaces;

public interface IVinylDapperRepository: IDapperRepository<Vinyl>
{
    Task<IEnumerable<Vinyl>> GetByComboAsync(string userId);
}
