using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IConcertDapperRepository : IDapperRepository<Concert>
{
    Task<ICollection<Concert>> GetUpcomingAsync(string userId);

    Task<ICollection<Concert>> GetPastAsync(string userId);
}
