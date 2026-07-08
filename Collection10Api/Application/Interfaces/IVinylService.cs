using Collection10Api.Application.Dtos;

namespace Collection10Api.Application.Interfaces;

public interface IVinylService : IService<VinylDto>
{
    Task<IEnumerable<VinylByComboDto>> GetByComboAsync(string userId);
    Task<VinylDto> PostAsync(VinylCreateDto vinylCreateDto, string userId);
    Task<VinylDto> PutAsync(Guid guid, VinylUpdateDto vinylUpdateDto, string userId);
}
