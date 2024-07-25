using Xunit;
using EduMaster.Domain.Repository;
using EduMaster.Infra.Repository.Memory;
using EduMaster.Application.Users.Create;
using EduMaster.Application.Command.CreateUser;
using EduMaster.Application.Query.GetUser;

namespace EduMaster.IntegrationTests;
public class Admin
{
    private readonly IUserRepository _userRepository;

    public Admin()
    {
        _userRepository = new UserRepositoryMemory();
    }

    [Fact]
    public async Task Should_Create_Admin()
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

       var getUserQuery = new GetUserQuery(outputCreateUser.Value);

       var getUserQueryHandler = new GetUserQueryHandler(_userRepository);

       var outputGetUser = await getUserQueryHandler.Handle(getUserQuery, CancellationToken.None);

       var createdUser = outputGetUser.Value;

       Assert.Equal("John Doe", createdUser.Name);
       Assert.Equal(email, createdUser.Email);
       Assert.Equal("(11) 94999-2100", createdUser.Phone);
       Assert.Null(createdUser.RegistrationNumber);
       Assert.Equal("568.661.720-12", createdUser.CPF);
       Assert.Equal("admin", createdUser.Role);
    }
}
