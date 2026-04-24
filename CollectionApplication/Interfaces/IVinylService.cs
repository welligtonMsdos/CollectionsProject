using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface IVinylService: IService<VinylDto>
{
    Task<IEnumerable<VinylByComboDto>> GetByComboAsync(string userId);
    Task<VinylDto> PostAsync(VinylCreateDto vinylCreateDto, string userId);
    Task<VinylDto> PutAsync(Guid guid, VinylUpdateDto vinylUpdateDto, string userId);
}
