using System.Reflection.Metadata.Ecma335;
using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Entity;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.CreateQuiz;
public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, Guid>
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IQuizRepository _quizRepository;

    public CreateQuizCommandHandler(IModuleRepository moduleRepository, IQuizRepository quizRepository)
    {
        _moduleRepository = moduleRepository;
        _quizRepository = quizRepository;
    }

    public async Task<Result<Guid>> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        if(request.ModuleId != null) 
        {
            var module = await _moduleRepository.GetByIdAsync(request.ModuleId.Value, cancellationToken);

            if(module == null) return Result.Failure<Guid>(ModuleErrors.ModuleNotFound); 
        }

        var quiz = Quiz.Create(request.Title, request.ModuleId);

        foreach(var question in request.Questions)
        {
            quiz.AddQuestion(question.Text, question.Points, question.Options, question.CorrectOptionIndex);
        }

        await _quizRepository.Save(quiz, cancellationToken);

        return quiz.Id;
    }
}