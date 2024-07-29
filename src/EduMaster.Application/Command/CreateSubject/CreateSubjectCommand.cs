using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Command.CreateSubject;
public record CreateSubjectCommand(string Name) : ICommand<Guid>;
