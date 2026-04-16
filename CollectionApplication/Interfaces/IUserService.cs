using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface IUserService
{
    Task<UserDto> PostAsync(UserCreateDto userCreateDto);
    Task<ICollection<UserDto>> GetAsync();
    Task<UserDto> GetByIdAsync(string id);
    Task<UserDto> GetByEmailAsync(string email);
    Task<UserDataLoginDto> GetDataLoginAsync(UserLoginDto userLoginDto);
    Task<UserDto> PutAsync(string id, UserUpdateDto userUpdated);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsByEmailAsync(string email);
}
