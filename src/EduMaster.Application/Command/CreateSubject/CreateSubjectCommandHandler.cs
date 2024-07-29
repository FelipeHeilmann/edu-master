using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Entity;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.CreateSubject;
public class CreateSubjectCommandHandler : ICommandHandler<CreateSubjectCommand, Guid>
{
    private readonly ISubjectRepository _subjectRepository;

    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<Result<Guid>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var existingSubject = await _subjectRepository.GetByNameAsync(request.Name, cancellationToken);

        if(existingSubject != null) return Result.Failure<Guid>(SubjectErrors.SubjectWithSameName);

        var subject = Subject.Create(request.Name);

        await _subjectRepository.Save(subject, cancellationToken);

        return Result.Success(subject.Id);
    }
}
