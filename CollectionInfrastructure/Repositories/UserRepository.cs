using CollectionDomain.Entities;
using CollectionDomain.Interfaces;
using CollectionInfrastructure.Data;
using MongoDB.Driver;

namespace CollectionInfrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthContext _context;

    public UserRepository(AuthContext context)
    {
        _context = context;
    }

    public async Task<bool> AddOutboxMessage(OutboxMessage outboxMessage, 
                                             IClientSessionHandle session)
    {
        try
        {
            await _context.OutboxMessages.InsertOneAsync(session, outboxMessage);

            return true;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;

            return false;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            await _context.Users.UpdateOneAsync(u => u._id == id,
                                                Builders<User>.Update
                                .Set(u => u.Active, false));

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
                             .Find(u => u.Email == email)
                             .AnyAsync();
    }

    public async Task<ICollection<User>> GetAsync()
    {
        var users = await _context.Users.Find(u => u.Active).ToListAsync();

        return users;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _context.Users
                                 .Find(u => u.Email == email && u.Active)
                                 .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var user = await _context.Users
                                 .Find(u => u._id == id && u.Active)
                                 .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> PostAsync(User user, IClientSessionHandle session)
    {
        await _context.Users.InsertOneAsync(session, user);

        return user;
    }

    public async Task<User> PutAsync(User obj)
    {
        await _context.Users.UpdateOneAsync(
            u => u._id == obj._id,
            Builders<User>.Update
                .Set(u => u.Name, obj.Name)
                .Set(u => u.Email, obj.Email)
                .Set(u => u.LastAccess, DateTime.Now));

        var user = await _context.Users.Find(u => u._id == obj._id).FirstOrDefaultAsync();

        return user;
    }
}
