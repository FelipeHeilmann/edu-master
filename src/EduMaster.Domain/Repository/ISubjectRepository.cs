using EduMaster.Domain.Entity;
using EduMaster.Domain.Shared;

namespace EduMaster.Domain.Repository;
public interface ISubjectRepository : IRepository<Subject>
{
    public Task<Subject?> GetByNameAsync(string name, CancellationToken cancellationToken);    
}
