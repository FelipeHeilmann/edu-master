using EduMaster.Application.Command.CreateModule;
using EduMaster.Application.Command.CreateQuiz;
using EduMaster.Application.Command.CreateSubject;
using EduMaster.Application.Command.CreateUser;
using EduMaster.Application.Query.GetQuiz;
using EduMaster.Application.Users.Create;
using EduMaster.Domain.Repository;
using EduMaster.Infra.Repository.Memory;
using Xunit;

namespace EduMaster.IntegrationTests;
public class Quiz
{
    private readonly IQuizRepository _quizRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IModuleRepository _moduleRepository;

    public Quiz()
    {
        _quizRepository = new QuizRepositoryMemory();
        _subjectRepository = new SubjectRepositoryMemory();
        _userRepository = new UserRepositoryMemory();
        _moduleRepository = new ModuleRepositoryMemory();
    }

    [Fact]
    public async Task Sould_Create_Quiz()
    {
        var email = $"john.doe{new Random().NextInt64()}@gmail.com";

        var createUserCommand = new CreateUserCommand(
                                    Name:"John Doe", 
                                    Email:email,
                                    Password:"password", 
                                    Phone:"(11) 94999-2100", 
                                    CPF:"568.661.720-12", 
                                    BirthDate: new DateTime(2004, 6, 11),
                                    EnrollmentDate: DateTime.UtcNow,
                                    Role: "admin");
                                    
        var createUserCommandHandler = new CreateUserCommandHandler(_userRepository);

        var outputCreateUser = await createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

        var createSubjectCommand = new CreateSubjectCommand("Geral");

        var createSubjectCommandHandler = new CreateSubjectCommandHandler(_subjectRepository);

        var outputCreateSubject = await createSubjectCommandHandler.Handle(createSubjectCommand, CancellationToken.None);

        var createModuleCommand = new CreateModuleCommand("Módulo 1", "Descricao", outputCreateSubject.Value);

        var createModuleCommandHandler = new CreateModuleCommandHandler(_moduleRepository);

        var outputCreateModule = await createModuleCommandHandler.Handle(createModuleCommand, CancellationToken.None);

        var createQuizQuestionCommand = new List<CreateQuizQuestionCommand>()
        {
            new CreateQuizQuestionCommand("Qual é o maior país da América do Sul?", 0.4, new List<string>()
            {
                "Brasil",
                "Argentina",
                "Colômbia"
            }, 0),

            new CreateQuizQuestionCommand("Qual é o rio mais longo do mundo?", 0.5, new List<string>()
            {
                "Rio Amazonas",
                "Rio Nilo",
                "Rio Mississipi"
            }, 1),

            new CreateQuizQuestionCommand("Em qual continente está localizado o Deserto do Saara?", 0.3, new List<string>()
            {
                "Ásia",
                "África",
                "América do Norte"
            }, 1),

            new CreateQuizQuestionCommand("Qual é a capital da Austrália?", 0.4, new List<string>()
            {
                "Sydney",
                "Melbourne",
                "Canberra"
            }, 2),

            new CreateQuizQuestionCommand("Qual é o menor país do mundo em termos de área?", 0.2, new List<string>()
            {
                "Mônaco",
                "Vaticano",
                "San Marino"
            }, 1),
        };
        
        var createQuizCommand = new CreateQuizCommand("Quiz 1", createQuizQuestionCommand, outputCreateModule.Value);

        var createQuizCommandHandler = new CreateQuizCommandHandler(_moduleRepository, _quizRepository);

        var ouputCreateQuiz = await createQuizCommandHandler.Handle(createQuizCommand, CancellationToken.None);

        var getQuizQuery = new GetQuizQuery(ouputCreateQuiz.Value);

        var getQuizQueryHandler = new GetQuizQueryHandler(_quizRepository);

        var outputGetQuiz = await getQuizQueryHandler.Handle(getQuizQuery, CancellationToken.None);

        var quiz = outputGetQuiz.Value;

        Assert.Equal("Quiz 1", quiz.Title);
        Assert.Equal(5, quiz.Questions.ToList().Count);
        Assert.Equal("Qual é o maior país da América do Sul?", quiz.Questions.ToList()[0].Text); 
        Assert.Equal(0.4, quiz.Questions.ToList()[0].Points); 
        Assert.Equal(3, quiz.Questions.ToList()[0].Options.Count()); 
        Assert.Equal(1, quiz.Questions.ToList()[0].Options.ToList()[0].Number);
        Assert.Equal("Brasil", quiz.Questions.ToList()[0].Options.ToList()[0].Text);
        Assert.Equal(0, quiz.Questions.ToList()[0].CorrectOptionIndex); 
        Assert.Equal("Qual é o menor país do mundo em termos de área?", quiz.Questions.ToList()[^1].Text); 
        Assert.Equal(0.2, quiz.Questions.ToList()[^1].Points); 
        Assert.Equal(3, quiz.Questions.ToList()[^1].Options.Count()); 
        Assert.Equal(1, quiz.Questions.ToList()[^1].Options.ToList()[0].Number);
        Assert.Equal("Mônaco", quiz.Questions.ToList()[^1].Options.ToList()[0].Text);
        Assert.Equal(1, quiz.Questions.ToList()[^1].CorrectOptionIndex); 
    }
        
}
