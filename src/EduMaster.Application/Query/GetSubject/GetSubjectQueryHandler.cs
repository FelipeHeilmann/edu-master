using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Query.GetSubject;
public class GetSubjectQueryHandler : IQueryHandler<GetSubjectQuery, Output>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectQueryHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<Output>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.Id, cancellationToken);

        if(subject == null) return Result.Failure<Output>(SubjectErrors.SubjectNotFound);

        return new Output(
            subject.Id,
            subject.Name,
            subject.CreatedAt
        );
    }
}
