namespace EduMaster.Domain.Entity;
public class Question
{
    public Guid Id { get; private set; }
    public string Text { get; private set; } = string.Empty;
    public double Points { get; private set; }
    public ICollection<string> Options { get; private set; } = new List<string>();
    public int CorrectOptionIndex { get; private set; }

    private Question(Guid id, string text, double points, ICollection<string> options, int correctOptionIndex)
    {
        Id = id;
        Text = text;
        Points = points;
        Options = options;
        CorrectOptionIndex = correctOptionIndex;
    }    

    public static Question Create(string text, double points, ICollection<string> options, int correctOptionIndex)
    {
        var id = Guid.NewGuid();

        return new Question(id, text, points, options, correctOptionIndex);
    }
}
