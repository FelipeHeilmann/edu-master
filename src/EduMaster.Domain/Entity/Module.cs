namespace EduMaster.Domain.Entity;
public class Module
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid SubjectId { get; private set; }
    public ICollection<Lesson> Lessons { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Module(Guid id, string name, string description, Guid subjectId, DateTime createdAt, ICollection<Lesson> leasons)
    {
        Id = id;
        Name = name;
        Description = description;
        SubjectId = subjectId;
        CreatedAt = createdAt;
        Lessons = leasons;
    }

    public static Module Create(string name, string description, Guid subjectId)
    {
        return new Module(Guid.NewGuid(), name, description, subjectId, DateTime.UtcNow, new List<Lesson>());
    }

    public void AddLesson(string title, ICollection<ContentDto> contents)
    {
        var lesson = Lesson.Create(title, Id);

        foreach(var content in contents)
        {
            lesson.AddContent(content.title, content.description, content.contentType, content.contentUrl);
        }

        Lessons.Add(lesson);
    }
        
}

public record ContentDto(string title, string description, string contentType, string contentUrl);
