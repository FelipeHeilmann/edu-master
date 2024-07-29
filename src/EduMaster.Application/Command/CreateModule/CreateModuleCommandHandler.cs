using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Entity;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.CreateModule;
public class CreateModuleCommandHandler : ICommandHandler<CreateModuleCommand, Guid>
{
    private readonly IModuleRepository _moduleRepository;

    public CreateModuleCommandHandler(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<Result<Guid>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var existingModule = await _moduleRepository.GetByNameAsync(request.Name, cancellationToken);

        if(existingModule != null) return Result.Failure<Guid>(ModuleErrors.ModuleWithSameName);

        var module = Module.Create(request.Name, request.Description, request.SubjectId);

        await _moduleRepository.Save(module, cancellationToken);
        
        return module.Id;
    }
}
