namespace EduMaster.Domain.Errors;
public class SubjectErrors
{
    public static Shared.Error SubjectWithSameName => Shared.Error.Conflict("Subject.With.Same.Name", "There is already a subject with this name");
    public static Shared.Error SubjectNotFound => Shared.Error.NotFound("Subject.Not.Found","The subject was not found");
}
