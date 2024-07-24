using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Application.Users.Create;
using EduMaster.Domain.Entity;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.CreateUser;
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if(existingUser != null) return Result.Failure<Guid>(UserErrors.EmailIsAreadyUsed);

        var user = User.Create(request.Name, request.Email, request.Password, request.Phone, request.CPF, request.Role, request.BirthDate, request.EnrollmentDate);

        await _userRepository.SaveAsync(user, cancellationToken);

        return Result.Success(user.Id);        
    }
}
