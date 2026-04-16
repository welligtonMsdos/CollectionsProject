using CollectionApplication.Dtos;

namespace CollectionApplication.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(UserDataLoginDto userDataLoginDto, string key);
}
