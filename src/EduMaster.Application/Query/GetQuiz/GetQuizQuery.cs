using EduMaster.Application.Abstractions.Messaging;

namespace EduMaster.Application.Query.GetQuiz;

public record OptionOutput(int Number, string Text);
public record QuestionOutput(Guid Id, string Text, double Points, IEnumerable<OptionOutput> Options, int CorrectOptionIndex);
public record Output(Guid Id, string Title, Guid? ModuleId ,IEnumerable<QuestionOutput> Questions);
public record GetQuizQuery(Guid Id) : IQuery<Output>;


