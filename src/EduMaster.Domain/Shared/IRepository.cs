
namespace EduMaster.Domain.Shared;
public interface IRepository<T>
{
    public Task<ICollection<T>> ListAsync(CancellationToken cancellationToken);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task Save(T entity, CancellationToken cancellationToken);
    public Task Update(T entity, CancellationToken cancellationToken);
    public Task Delete(T entity, CancellationToken cancellationToken);
}
