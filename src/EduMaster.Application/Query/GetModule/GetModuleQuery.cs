using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.GetModule;

public record SubjectOutput(Guid Id, string Name);
public record Output(Guid Id, string Name, string Description, DateTime createdAt, SubjectOutput Subject);
public record GetModuleQuery(Guid Id) : IQuery<Output>;
