namespace EduMaster.Domain.Entity;
public class Quiz
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid? ModuleId { get; private set; }
    public ICollection<Question> Questions = new List<Question>();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Quiz(Guid id, string title, Guid? moduleId ,ICollection<Question> questions, DateTime createdAt)
    {
        Id = id;
        Title = title;
        ModuleId = moduleId;
        Questions = questions;
        CreatedAt = createdAt;
    }       

    public static Quiz Create(string title, Guid? moduleId)
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        return new Quiz(id, title, moduleId, new List<Question>(), createdAt);
    }

    public void AddQuestion(string text, double points, ICollection<string> options, int correctOptionIndex)
    {
        Questions.Add(Question.Create(text, points, options, correctOptionIndex));
    }
}