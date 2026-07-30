using Collection10Api.Application.Dtos;

namespace Collection10Api.Application.Interfaces;

public interface IConcertService 
{
    Task<ConcertDto> CreateConcertAsync(ConcertCreateDto dto, string userId);
    Task<ICollection<ConcertDto>> GetConcertsAsync(string userId);
    Task<ICollection<ConcertDto>> GetUpcomingAsync(string userId);
    Task<ICollection<ConcertDto>> GetPastAsync(string userId);
    Task<ConcertDto> GetConcertByGuidAsync(Guid guid);
    Task<ConcertDto> UpdateConcertAsync(Guid guid, ConcertUpdateDto dto, string userId);
    Task<bool> DeleteConcertAsync(Guid guid);  
}
