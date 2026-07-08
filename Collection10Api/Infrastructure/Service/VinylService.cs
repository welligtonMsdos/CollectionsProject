using Collection10Api.Application.Dtos;
using Collection10Api.Application.Extensions;
using Collection10Api.Application.Interfaces;
using Collection10Api.Domain.Interfaces;

namespace Collection10Api.Infrastructure.Service;

public class VinylService : IVinylService
{
    private readonly IVinylDapperRepository _repository;
    private readonly IVinylEFRepository _efRepository;

    public VinylService(IVinylDapperRepository repository,
                        IVinylEFRepository efRepository)
    {
        _repository = repository;
        _efRepository = efRepository;        
    }

    public async Task<bool> DeleteAsync(Guid guid)
    {
        var vinylEntity = await _repository.GetByGuidAsync(guid);

        if (vinylEntity == null) return false;

        return await _efRepository.DeleteAsync(vinylEntity);
    }

    public async Task<ICollection<VinylDto>> GetAsync(string userId)
    {
        var vinyls = await _repository.GetAsync(userId);

        ArgumentNullException.ThrowIfNull(vinyls);

        return vinyls.Select(v => v.ToVinylDto()).ToList();
    }

    public async Task<IEnumerable<VinylByComboDto>> GetByComboAsync(string userId)
    {
        var vinyls = await _repository.GetByComboAsync(userId);

        ArgumentNullException.ThrowIfNull(vinyls);

        return vinyls.Select(v => v.ToVinylByComboDto()).ToList();
    }

    public async Task<VinylDto> GetByGuidAsync(Guid guid)
    {
        var vinyl = await _repository.GetByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(vinyl);

        return vinyl.ToVinylDto();
    }

    public async Task<VinylDto> PostAsync(VinylCreateDto vinylCreateDto, string userId)
    {
        var vinyl = vinylCreateDto.ToEntity();

        vinyl.Active = true;

        vinyl.UserId = userId;

        var createdVinyl = await _efRepository.PostAsync(vinyl);

        return createdVinyl.ToVinylDto();
    }

    public async Task<VinylDto> PutAsync(Guid guid, VinylUpdateDto vinylUpdateDto, string userId)
    {
        var vinyl = await _repository.GetByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(vinyl);

        vinyl.UpdateEntity(vinylUpdateDto);

        vinyl.Active = true;

        vinyl.UserId = userId;

        await _efRepository.PutAsync(vinyl);

        return vinyl.ToVinylDto();
    }
}
