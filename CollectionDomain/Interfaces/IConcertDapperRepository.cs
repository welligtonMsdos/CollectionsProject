using CollectionDomain.Entities;

namespace CollectionDomain.Interfaces;

public interface IConcertDapperRepository : IDapperRepository<Concert>
{
    Task<ICollection<Concert>> GetUpcomingAsync(string userId);

    Task<ICollection<Concert>> GetPastAsync(string userId);
}
