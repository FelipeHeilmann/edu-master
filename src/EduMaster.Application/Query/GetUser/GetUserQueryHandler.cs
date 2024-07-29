using EduMaster.Application.Abstractions.Messaging;
using EduMaster.Domain.Errors;
using EduMaster.Domain.Repository;
using EduMaster.Domain.Shared;

namespace EduMaster.Application.Query.GetUser;
public class GetUserQueryHandler : IQueryHandler<GetUserQuery, Output>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Output>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if(user == null) return Result.Failure<Output>(UserErrors.UserNotFound);

        return new Output(
                Id: user.Id,
                Name: user.Name,
                Email: user.Email,
                Phone: user.PhoneFormatted,
                CPF: user.CPFFormatted,
                Status: user.Status,
                RegistrationNumber: user.RegistrationNumber,
                Role: user.Role,
                BirthDate: user.BirthDate.ToString("dd/MM/YYYY"),
                EnrollmentDate: user.EnrollmentDate.ToString("dd/MM/YYYY")
        );
    }
}
