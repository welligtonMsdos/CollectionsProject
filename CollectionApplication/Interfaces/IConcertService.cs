using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface IConcertService : IService<ConcertDto>
{
    Task<ICollection<ConcertDto>> GetUpcomingAsync(string userId);

    Task<ICollection<ConcertDto>> GetPastAsync(string userId);

    Task<ConcertDto> PostAsync(ConcertCreateDto dto, string userId);

    Task<ConcertDto> PutAsync(Guid guid, ConcertUpdateDto dto, string userId);
}
