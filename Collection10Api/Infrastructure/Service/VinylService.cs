using Collection10Api.Application.Dtos;
using Collection10Api.Application.Extensions;
using Collection10Api.Application.Interfaces;
using Collection10Api.Domain.Interfaces;

namespace Collection10Api.Infrastructure.Service;

public class VinylService : IVinylService
{
    private readonly IVinylDapperRepository _vinylRepository;
    private readonly IVinylEFRepository _efVinylRepository;

    public VinylService(IVinylDapperRepository vinylRepository,
                        IVinylEFRepository efVinylRepository)
    {
        _vinylRepository = vinylRepository;
        _efVinylRepository = efVinylRepository;        
    }

    public async Task<VinylDto> CreateVinylAsync(VinylCreateDto vinylCreateDto, string userId)
    {
        var vinyl = vinylCreateDto.ToEntity();

        vinyl.Active = true;

        vinyl.UserId = userId;

        var createdVinyl = await _efVinylRepository.CreateVinylAsync(vinyl);

        return createdVinyl.ToVinylDto();
    }

    public async Task<ICollection<VinylDto>> GetVinylsAsync(string userId)
    {
        var vinyls = await _vinylRepository.GetVinylsAsync(userId);

        ArgumentNullException.ThrowIfNull(vinyls);

        return vinyls.Select(v => v.ToVinylDto()).ToList();
    }

    public async Task<IEnumerable<VinylByComboDto>> GetVinylByComboAsync(string userId)
    {
        var vinyls = await _vinylRepository.GetVinylByComboAsync(userId);

        ArgumentNullException.ThrowIfNull(vinyls);

        return vinyls.Select(v => v.ToVinylByComboDto()).ToList();
    }

    public async Task<VinylDto> GetVinylByGuidAsync(Guid guid)
    {
        var vinyl = await _vinylRepository.GetVinylByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(vinyl);

        return vinyl.ToVinylDto();
    }

    public async Task<VinylDto> UpdateVinylAsync(Guid guid, VinylUpdateDto vinylUpdateDto, string userId)
    {
        var vinyl = await _vinylRepository.GetVinylByGuidAsync(guid);

        ArgumentNullException.ThrowIfNull(vinyl);

        vinyl.UpdateEntity(vinylUpdateDto);

        vinyl.Active = true;

        vinyl.UserId = userId;

        await _efVinylRepository.UpdateVinylAsync(vinyl);

        return vinyl.ToVinylDto();
    }

    public async Task<bool> DeleteVinylAsync(Guid guid)
    {
        var vinylEntity = await _vinylRepository.GetVinylByGuidAsync(guid);

        if (vinylEntity == null) return false;

        return await _efVinylRepository.DeleteVinylAsync(vinylEntity);
    } 
}
