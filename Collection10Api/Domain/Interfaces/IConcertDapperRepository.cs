using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IConcertDapperRepository 
{
    Task<IEnumerable<Concert>> GetConcertsAsync(string userId);
    Task<ICollection<Concert>> GetUpcomingAsync(string userId);
    Task<ICollection<Concert>> GetPastAsync(string userId);
    Task<Concert?> GetConcertByGuidAsync(Guid guid);     
}
