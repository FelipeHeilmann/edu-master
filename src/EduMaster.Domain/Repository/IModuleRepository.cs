using EduMaster.Domain.Entity;
using EduMaster.Domain.Shared;

namespace EduMaster.Domain.Repository;
public interface IModuleRepository : IRepository<Module>
{
    public Task<Module?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
