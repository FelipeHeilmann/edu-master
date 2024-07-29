using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Query.ListModulesBySubject;
public class ListModulesBySubjectQueryHandler : IQueryHandler<ListModulesBySubjectQuery, IEnumerable<Output>>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ISubjectRepository _subjectRepository;

    public ListModulesBySubjectQueryHandler(IModuleRepository moduleRepository, ISubjectRepository subjectRepository)
    {
        _moduleRepository = moduleRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<IEnumerable<Output>>> Handle(ListModulesBySubjectQuery request, CancellationToken cancellationToken)
    {
        var modules = await _moduleRepository.ListBySubjectIdAsync(request.SubjectId, cancellationToken);

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId, cancellationToken);

        if(subject == null) return Result.Failure<IEnumerable<Output>>(SubjectErrors.SubjectNotFound);

        return Result.Success(modules.Select(module =>
            new Output(module.Id, 
                       module.Name, 
                       module.Description, 
                       module.CreatedAt, 
                       new SubjectOutput(subject.Id, subject.Name))));
    }
}
