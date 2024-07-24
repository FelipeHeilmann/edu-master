
namespace EduMaster.Domain.Shared;
public record Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure); //static propertity is allocate to a heap and has the same value for all of instances

    public static readonly Error NullValue = new(
      "General.Null",
      "Null value was provided",
      ErrorType.Failure);

    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string description) => new(code, description, ErrorType.Validation);
    public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
    public static Error Problem(string code, string description) => new(code, description, ErrorType.Problem);
    public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2, 
    Conflict = 3,
    Problem = 4,
}
