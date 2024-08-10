using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Command.AddLeasonToModule;
public record LessonContentCommand(string Title, string Description, string ContentType, string ContentUrl);
public record AddLessonToModuleCommand(Guid ModuleId, string Title, ICollection<LessonContentCommand> Contents) : ICommand;
