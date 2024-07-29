using EduMaster.Application.Command.CreateModule;
using EduMaster.Application.Command.CreateSubject;
using EduMaster.Application.Command.CreateUser;
using EduMaster.Application.Query.GetModule;
using EduMaster.Application.Query.GetSubject;
using EduMaster.Application.Users.Create;
using EduMaster.Domain.Repository;
using EduMaster.Infra.Repository.Memory;
using Xunit;

namespace EduMaster.IntegrationTests;
public class Module
{
    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IModuleRepository _moduleRepository;

    public Module()
    {
        _userRepository = new UserRepositoryMemory();
        _subjectRepository = new SubjectRepositoryMemory();
        _moduleRepository = new ModuleRepositoryMemory();
    }

    [Fact]
    public async Task Should_Create_Subject_And_Add_Module()
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

        var createModuleCommand = new CreateModuleCommand("Módulo 1", "Descricao", outputCreateSubject.Value);

        var createModuleCommandHandler = new CreateModuleCommandHandler(_moduleRepository);

        var outputCreateModule = await createModuleCommandHandler.Handle(createModuleCommand, CancellationToken.None);

        var getModuleQuery = new GetModuleQuery(outputCreateModule.Value);

        var getModuleQueryHandler = new GetModuleQueryHandler(_moduleRepository, _subjectRepository);

        var outputGetModule = await getModuleQueryHandler.Handle(getModuleQuery, CancellationToken.None);

        var module = outputGetModule.Value;

        Assert.Equal("Módulo 1", module.Name);
        Assert.Equal("Descricao", module.Description);
        Assert.Equal("Física", module.Subject.Name);
        Assert.Equal(outputCreateSubject.Value, module.Subject.Id);
    }
}     
