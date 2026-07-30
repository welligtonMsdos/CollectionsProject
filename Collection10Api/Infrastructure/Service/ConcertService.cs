using Collection10Api.Application.Dtos;
using Collection10Api.Application.Extensions;
using Collection10Api.Application.Interfaces;
using Collection10Api.Domain.Interfaces;

namespace Collection10Api.Infrastructure.Service;

public class ConcertService : IConcertService
{
    private readonly IConcertDapperRepository _concertRepository;
    private readonly IConcertEFRepository _efConcertRepository;

    public ConcertService(IConcertDapperRepository concertRepository,
                         IConcertEFRepository efConcertRepository)
    {
        _concertRepository = concertRepository;
        _efConcertRepository = efConcertRepository;      
    }

    public async Task<ConcertDto> CreateConcertAsync(ConcertCreateDto dto, string userId)
    {
        var concert = dto.ToEntity();

        concert.Active = true;

        concert.UserId = userId;

        var createdConcert = await _efConcertRepository.CreateConcertAsync(concert);

        return createdConcert.ToConcertDto();
    }

    public async Task<ICollection<ConcertDto>> GetConcertsAsync(string userId)
    {
        var concerts = await _concertRepository.GetConcertsAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ICollection<ConcertDto>> GetUpcomingAsync(string userId)
    {
        var concerts = await _concertRepository.GetUpcomingAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ICollection<ConcertDto>> GetPastAsync(string userId)
    {
        var concerts = await _concertRepository.GetPastAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ConcertDto> GetConcertByGuidAsync(Guid guid)
    {
        var concert = await _concertRepository.GetConcertByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(concert);

        return concert.ToConcertDto();
    }

    public async Task<ConcertDto> UpdateConcertAsync(Guid guid, ConcertUpdateDto dto, string userId)
    {
        var concert = await _concertRepository.GetConcertByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(concert);

        concert.UpdateEntity(dto);

        concert.Active = true;

        concert.UserId = userId;

        await _efConcertRepository.UpdateConcertAsync(concert);

        return concert.ToConcertDto();
    }

    public async Task<bool> DeleteConcertAsync(Guid guid)
    {
        var concert = await _concertRepository.GetConcertByGuidAsync(guid);

        if (concert == null) return false;

        return await _efConcertRepository.DeleteConcertAsync(concert);
    } 
}
