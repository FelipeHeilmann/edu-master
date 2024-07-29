using Xunit;
using EduMaster.Domain.Repository;
using EduMaster.Infra.Repository.Memory;
using EduMaster.Application.Users.Create;
using EduMaster.Application.Command.CreateUser;
using EduMaster.Application.Command.CreateSubject;
using EduMaster.Application.Query.GetSubject;

namespace EduMaster.IntegrationTests;
public class Subject
{
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;

    public Subject()
    {
        _userRepository = new UserRepositoryMemory();
        _subjectRepository = new SubjectRepositoryMemory();
    }

    [Fact]
    public async Task Should_Create_Subject()
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

        var createSubjectCommand = new CreateSubjectCommand("Física");

        var createSubjectCommandHandler = new CreateSubjectCommandHandler(_subjectRepository);

        var outputCreateSubject = await createSubjectCommandHandler.Handle(createSubjectCommand, CancellationToken.None);

        var getSubjectQuery = new GetSubjectQuery(outputCreateSubject.Value);

        var getSubjectQueryHandler = new GetSubjectQueryHandler(_subjectRepository);

        var outputGetSubject = await getSubjectQueryHandler.Handle(getSubjectQuery, CancellationToken.None);

        Assert.Equal("Física", outputGetSubject.Value.Name);
    }
}
