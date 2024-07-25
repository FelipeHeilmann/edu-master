using EduMaster.Domain.Entity;
using EduMaster.Domain.Shared;

namespace EduMaster.Domain.Repository;
public interface IUserRepository : IRepository<User> 
{
    public Task<User?> GetByEmailOrRegistrationNumberAsync(string emailOrRegistrationNumber, CancellationToken cancellationToken);
}
