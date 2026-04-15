using CollectionDomain.Dtos.Users;

namespace CollectionDomain.Interfaces.Services;

public interface ITokenService
{
    Task<string> GenerateToken(UserDataLoginDto userDataLoginDto, string key);
}
