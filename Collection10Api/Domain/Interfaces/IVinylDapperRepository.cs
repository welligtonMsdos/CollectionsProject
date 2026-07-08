using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IVinylDapperRepository : IDapperRepository<Vinyl>
{
    Task<IEnumerable<Vinyl>> GetByComboAsync(string userId);
}
