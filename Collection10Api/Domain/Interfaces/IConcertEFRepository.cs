using Collection10Api.Domain.Entities;

namespace Collection10Api.Domain.Interfaces;

public interface IConcertEFRepository 
{
    Task<Concert> CreateConcertAsync(Concert obj);
    Task<Concert> UpdateConcertAsync(Concert obj);
    Task<bool> DeleteConcertAsync(Concert obj);
}
