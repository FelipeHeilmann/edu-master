using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Command.CreateQuiz;

public record CreateQuizQuestionCommand(string Text, double Points, ICollection<string> Options, int CorrectOptionIndex);
public record CreateQuizCommand(string Title, List<CreateQuizQuestionCommand> Questions, Guid? ModuleId) : ICommand<Guid>;
