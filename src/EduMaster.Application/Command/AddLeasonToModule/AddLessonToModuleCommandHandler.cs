using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Entity;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.AddLeasonToModule;
public class AddLesssonToModuleCommandHandler : ICommandHandler<AddLessonToModuleCommand>
{
    private readonly IModuleRepository _moduleRepository;

    public AddLesssonToModuleCommandHandler(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<Result> Handle(AddLessonToModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(request.ModuleId, cancellationToken);

        if(module == null) return Result.Failure<Guid>(ModuleErrors.ModuleNotFound);
        
        module.AddLesson(request.Title, request.Contents.Select(content => 
                                                                new ContentDto(content.Title, 
                                                                               content.Description, 
                                                                               content.ContentType, 
                                                                               content.ContentUrl)).ToList());

        await _moduleRepository.Update(module, cancellationToken);
        
        return Result.Success();
    }
}
