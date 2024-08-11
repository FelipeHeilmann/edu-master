using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Query.GetQuiz;
public class GetQuizQueryHandler : IQueryHandler<GetQuizQuery, Output>
{
    private readonly IQuizRepository _quizRepository;

    public GetQuizQueryHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<Result<Output>> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByIdAsync(request.Id, cancellationToken);

        if(quiz == null) return Result.Failure<Output>(QuizErrors.QuizNotFound);

        return new Output(quiz.Id, 
                          quiz.Title, 
                          quiz.ModuleId, 
                          quiz.Questions.Select(question => new QuestionOutput(question.Id, 
                                                                               question.Text, 
                                                                               question.Points, 
                                                                               question.Options.Select((question, index)  => new OptionOutput(++index, question)), 
                                                                               question.CorrectOptionIndex)));
    }
}
