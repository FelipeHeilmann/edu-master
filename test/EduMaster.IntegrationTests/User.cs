using System.Timers;
using EduMaster.Application.Abstractions.Service;
using EduMaster.Application.Command.CreateUser;
using EduMaster.Application.Command.Login;
using EduMaster.Application.Query.GetUser;
using EduMaster.Application.Users.Create;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Infra.Authentication;
using EduMaster.Infra.Repository.Memory;
using Microsoft.Extensions.Options;
using Xunit;

namespace EduMaster.IntegrationTests;
public class User
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public User()
    {
        _userRepository = new UserRepositoryMemory();
        var jwtOptions = new JwtOptions
        {
            SecretKey = "arweuhsahngvuishnbgvrsgwehvrswguohwrskgihbwgsdzknvbosbfodibsagd",
            Issuer = "EduMaster",
            Audience = "EduMaster"
        };

        var options = new OptionsWrapper<JwtOptions>(jwtOptions);

        _tokenService = new JwtService(options);
    }       

    [Fact]
    public async Task Should_Create_Professor_Account()
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
                                    Role: "professor");
        
       var createUserCommandHandler = new CreateUserCommandHandler(_userRepository);

       var outputCreateUser = await createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

       var getUserQuery = new GetUserQuery(outputCreateUser.Value);

       var getUserQueryHandler = new GetUserQueryHandler(_userRepository);

       var outputGetUser = await getUserQueryHandler.Handle(getUserQuery, CancellationToken.None);

       var createdUser = outputGetUser.Value;

       Assert.Equal("John Doe", createdUser.Name);
       Assert.Equal(email, createdUser.Email);
       Assert.Equal("(11) 94999-2100", createdUser.Phone);
       Assert.Contains("P", createdUser.RegistrationNumber);
       Assert.Equal("568.661.720-12", createdUser.CPF);
       Assert.Equal("professor", createdUser.Role);
    }

    [Fact]
    public async void Should_Create_Student() 
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
                                    Role: "student");
        
       var createUserCommandHandler = new CreateUserCommandHandler(_userRepository);

       var outputCreateUser = await createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

       var getUserQuery = new GetUserQuery(outputCreateUser.Value);

       var getUserQueryHandler = new GetUserQueryHandler(_userRepository);

       var outputGetUser = await getUserQueryHandler.Handle(getUserQuery, CancellationToken.None);

       var createdUser = outputGetUser.Value;

       Assert.Equal("John Doe", createdUser.Name);
       Assert.Equal(email, createdUser.Email);
       Assert.Contains("RM", createdUser.RegistrationNumber);
       Assert.Equal("(11) 94999-2100", createdUser.Phone);
       Assert.Equal("568.661.720-12", createdUser.CPF);
       Assert.Equal("student", createdUser.Role);
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

    [Fact]
    public async Task Should_Login_Student()
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
                                    Role: "student");
        var createUserCommandHandler = new CreateUserCommandHandler(_userRepository);

        await createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

        var loginCommand = new LoginCommand(email, "password");

        var loginCommandHandler = new LoginCommandHandler(_userRepository, _tokenService);

        var outputLogin = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);

        var token = outputLogin.Value;

        Assert.NotNull(token);
    }

    [Fact]
    public async Task Should_Not_Login_With_Invalid_Credential()
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
                                    Role: "student");
        var createUserCommandHandler = new CreateUserCommandHandler(_userRepository);

        await createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

        var loginCommand = new LoginCommand(email, "passwor");

        var loginCommandHandler = new LoginCommandHandler(_userRepository, _tokenService);

        var outputLogin = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);

        Assert.Equal(UserErrors.InvalidCredencials, outputLogin.Error);
        Assert.True(outputLogin.IsFailure);
    }
}
