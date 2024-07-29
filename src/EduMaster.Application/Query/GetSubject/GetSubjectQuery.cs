using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.GetSubject;

public record Output(Guid Id, string Name, DateTime CreatedAt);
public record GetSubjectQuery(Guid Id) : IQuery<Output>;

