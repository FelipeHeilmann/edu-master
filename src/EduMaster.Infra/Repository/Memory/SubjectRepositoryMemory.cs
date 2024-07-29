using EduMaster.Domain.Entity;
using EduMaster.Domain.Repository;

namespace EduMaster.Infra.Repository.Memory;
public class SubjectRepositoryMemory : ISubjectRepository
{
    private readonly List<Subject> _subjects;

    public SubjectRepositoryMemory()
    {
        _subjects = [];
    }

    public Task<ICollection<Subject>> ListAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<ICollection<Subject>>(_subjects);
    }

    public Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_subjects.FirstOrDefault(subject => subject.Id == id));
    }

    public Task<Subject?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return Task.FromResult(_subjects.FirstOrDefault(subject => subject.Name == name));
    } 

    public Task Save(Subject entity, CancellationToken cancellationToken)
    {
        _subjects.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(Subject entity, CancellationToken cancellationToken)
    {
        var index = _subjects.FindIndex(subject => subject.Id == entity.Id);

        if(index != -1)
        {
            _subjects[index] = entity;
        }

        return Task.CompletedTask;
    }

    public Task Delete(Subject entity, CancellationToken cancellationToken)
    {
        _subjects.Remove(entity);
        return Task.CompletedTask;
    }
}
