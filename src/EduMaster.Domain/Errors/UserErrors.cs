namespace EduMaster.Domain.Errors;
public class UserErrors
{
    public static Shared.Error UserNotFound => Shared.Error.NotFound("User.Not.Found", "The user was not found");
    public static Shared.Error EmailIsAreadyUsed => Shared.Error.Failure("Email.Used", "The email is already used");
    public static Shared.Error InvalidCredencials => Shared.Error.Validation("Invalid.Credentials", "Invalid email or/and password");
}
