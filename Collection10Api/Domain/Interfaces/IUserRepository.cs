using Collection10Api.Domain.Entities;
using MongoDB.Driver;

namespace Collection10Api.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> AddOutboxMessage(OutboxMessage outboxMessage, IClientSessionHandle session);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User> PostAsync(User user, IClientSessionHandle session);
    Task<ICollection<User>> GetAsync();
    Task<User> GetByIdAsync(string id);
    Task<User> PutAsync(User obj);
    Task<bool> DeleteAsync(string id);
}
