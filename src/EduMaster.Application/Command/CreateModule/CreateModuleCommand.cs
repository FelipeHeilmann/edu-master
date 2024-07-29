using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Command.CreateModule;
public record CreateModuleCommand(string Name, string Description, Guid SubjectId) : ICommand<Guid>;

