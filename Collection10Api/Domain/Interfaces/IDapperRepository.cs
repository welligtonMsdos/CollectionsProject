namespace Collection10Api.Domain.Interfaces;

public interface IDapperRepository<T>
{
    Task<T?> GetByGuidAsync(Guid guid);
    Task<IEnumerable<T>> GetAsync(string userId);
}
