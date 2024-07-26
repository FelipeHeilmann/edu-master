using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Application.Abstractions.Service;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Command.Login;
public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailOrRegistrationNumberAsync(request.EmailOrRegistrationNumber, cancellationToken);

        if(user == null) return Result.Failure<string>(UserErrors.InvalidCredencials);

        if(!user.PasswordMatches(request.Password)) return Result.Failure<string>(UserErrors.InvalidCredencials);

        var token = _tokenService.GenerateAuthToken(user.Id, user.Name);

        return token;
    }
}
