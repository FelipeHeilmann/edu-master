using EduMaster.Domain.Entity;
using EduMaster.Domain.Repository;

namespace EduMaster.Infra.Repository.Memory;
public class ModuleRepositoryMemory : IModuleRepository
{
    private readonly List<Module> _modules;

    public ModuleRepositoryMemory()
    {
        _modules = [];
    }

    public Task<ICollection<Module>> ListAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<ICollection<Module>>(_modules);
    }

    public Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_modules.FirstOrDefault(module => module.Id == id));
    }

    public Task<Module?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return Task.FromResult(_modules.FirstOrDefault(module => module.Name == name));
    }

    public Task Save(Module entity, CancellationToken cancellationToken)
    {
        _modules.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(Module entity, CancellationToken cancellationToken)
    {
       var index = _modules.FindIndex(module => module.Id == entity.Id);

       if(index != -1)
       {
            _modules[index] = entity;
       }

       return Task.CompletedTask;
    }

    public Task Delete(Module entity, CancellationToken cancellationToken)
    {
        _modules.Remove(entity);
        return Task.CompletedTask;
    }
}
