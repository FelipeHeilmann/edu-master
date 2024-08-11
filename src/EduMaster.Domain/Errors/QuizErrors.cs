namespace EduMaster.Domain.Errors;
public static class QuizErrors
{
    public static Shared.Error QuizNotFound => Shared.Error.NotFound("Quiz.Not.Found", "The quiz was not found");        
}
