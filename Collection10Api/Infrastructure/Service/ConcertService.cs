using Collection10Api.Application.Dtos;
using Collection10Api.Application.Extensions;
using Collection10Api.Application.Interfaces;
using Collection10Api.Domain.Interfaces;

namespace Collection10Api.Infrastructure.Service;

public class ConcertService : IConcertService
{
    private readonly IConcertDapperRepository _repository;
    private readonly IConcertEFRepository _efRepository;

    public ConcertService(IConcertDapperRepository repository,
                         IConcertEFRepository efRepository)
    {
        _repository = repository;
        _efRepository = efRepository;      
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var concert = await _repository.GetByGuidAsync(guid);

        if (concert == null) return false;

        return await _efRepository.DeleteAsync(concert);
    }

    public async Task<ICollection<ConcertDto>> GetAsync(string userId)
    {
        var concerts = await _repository.GetAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ConcertDto> GetByGuidAsync(Guid guid)
    {
        var concert = await _repository.GetByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(concert);

        return concert.ToConcertDto();
    }

    public async Task<ICollection<ConcertDto>> GetPastAsync(string userId)
    {
        var concerts = await _repository.GetPastAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ICollection<ConcertDto>> GetUpcomingAsync(string userId)
    {
        var concerts = await _repository.GetUpcomingAsync(userId);

        ArgumentNullException.ThrowIfNull(concerts);

        return concerts.Select(c => c.ToConcertDto()).ToList();
    }

    public async Task<ConcertDto> PostAsync(ConcertCreateDto dto, string userId)
    {   
        var concert = dto.ToEntity();

        concert.Active = true;

        concert.UserId = userId;

        var createdConcert = await _efRepository.PostAsync(concert);

        return createdConcert.ToConcertDto();
    }

    public async Task<ConcertDto> PutAsync(Guid guid, ConcertUpdateDto dto, string userId)
    {
        var concert = await _repository.GetByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(concert);

        concert.UpdateEntity(dto);

        concert.Active = true;

        concert.UserId = userId;

        await _efRepository.PutAsync(concert);

        return concert.ToConcertDto();
    }
}
