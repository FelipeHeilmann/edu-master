
namespace EduMaster.Domain.Shared;
public interface IRepository<T>
{
    public Task<ICollection<T>> ListAsync(CancellationToken cancellationToken);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task SaveAsync(T entity, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
