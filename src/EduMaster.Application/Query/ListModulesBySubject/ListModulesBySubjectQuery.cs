using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.ListModulesBySubject;

public record SubjectOutput(Guid Id, string Name);
public record Output(Guid Id, string Name, string Description, DateTime createdAt, SubjectOutput Subject);
public record ListModulesBySubjectQuery(Guid SubjectId) : IQuery<IEnumerable<Output>>;
