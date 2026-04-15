using CollectionDomain.Dtos.Users;
using CollectionDomain.Models.Outbox;
using CollectionDomain.Models.Users;
using MongoDB.Driver;

namespace CollectionDomain.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetDataLoginAsync(UserLoginDto userLoginDto);
    Task<bool> AddOutboxMessage(OutboxMessage outboxMessage, IClientSessionHandle session);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User> PostAsync(User user, IClientSessionHandle session);
    Task<ICollection<User>> GetAsync();
    Task<User> GetByIdAsync(string id);
    Task<User> PutAsync(User obj);
    Task<bool> DeleteAsync(string id);
}
