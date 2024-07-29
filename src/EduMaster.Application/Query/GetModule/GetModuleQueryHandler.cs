using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Query.GetModule;
public class GetModuleQueryHandler : IQueryHandler<GetModuleQuery, Output>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetModuleQueryHandler(IModuleRepository moduleRepository, ISubjectRepository subjectRepository)
    {
        _moduleRepository = moduleRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<Output>> Handle(GetModuleQuery request, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(request.Id, CancellationToken.None);

        if(module == null) return Result.Failure<Output>(ModuleErrors.ModuleNotFound);

        var subject = await _subjectRepository.GetByIdAsync(module.SubjectId, cancellationToken);

        if(subject == null) return Result.Failure<Output>(SubjectErrors.SubjectNotFound);

        return new Output(
            module.Id,
            module.Name,
            module.Description,
            module.CreatedAt,
            new SubjectOutput(
                subject.Id,
                subject.Name
            )
        );
    }
}
