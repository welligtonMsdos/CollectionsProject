using Collection10Api.Application.Dtos;

namespace Collection10Api.Application.Interfaces;

public interface IVinylService
{
    Task<VinylDto> CreateVinylAsync(VinylCreateDto vinylCreateDto, string userId);
    Task<ICollection<VinylDto>> GetVinylsAsync(string userId);
    Task<IEnumerable<VinylByComboDto>> GetVinylByComboAsync(string userId);
    Task<VinylDto> GetVinylByGuidAsync(Guid guid);
    Task<VinylDto> UpdateVinylAsync(Guid guid, VinylUpdateDto vinylUpdateDto, string userId);
    Task<bool> DeleteVinylAsync(Guid guid); 
}
