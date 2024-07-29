using EduMaster.Domain.Entity;
using EduMaster.Domain.Repository;

namespace EduMaster.Infra.Repository.Memory;
public class UserRepositoryMemory : IUserRepository
{
    private readonly IList<User> _users;

    public UserRepositoryMemory()
    {
        _users = [];
    }

    public Task<ICollection<User>> ListAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<ICollection<User>>(_users);
    }

    public Task<User?> GetByEmailOrRegistrationNumberAsync(string emailOrRegistrationNumber, CancellationToken cancellationToken)
    {
        var user = _users.FirstOrDefault(user => user.Email == emailOrRegistrationNumber || user.RegistrationNumber == emailOrRegistrationNumber);
        return Task.FromResult(user);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = _users.FirstOrDefault(user => user.Id == id);
        return Task.FromResult(user);
    }

    public Task Save(User entity, CancellationToken cancellationToken)
    {
        _users.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(User entity, CancellationToken cancellationToken)
    {
        var index = _users.IndexOf(entity);
        
        if(index != -1) 
        {
            _users[index] = entity;
        }

        return Task.CompletedTask;
    }

    public Task Delete(User entity, CancellationToken cancellationToken)
    {
        _users.Remove(entity);
        return Task.CompletedTask;
    }
}
