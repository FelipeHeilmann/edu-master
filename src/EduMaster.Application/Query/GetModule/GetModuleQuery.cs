using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.GetModule;

public record SubjectOutput(Guid Id, string Name);
public record ContentOutput(Guid Id, string Title, string Description, string ContentType, string ContentUrl);
public record LessonsOutput(Guid Id, string Title, ICollection<ContentOutput> Contents);
public record Output(Guid Id, string Name, string Description, DateTime createdAt, SubjectOutput Subject, ICollection<LessonsOutput> Lessons);
public record GetModuleQuery(Guid Id) : IQuery<Output>;
