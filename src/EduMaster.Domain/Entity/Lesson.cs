namespace EduMaster.Domain.Entity;
public class Lesson
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid ModuleId { get; private set; }
    public ICollection<Content> Contents { get; private set; }

    private Lesson(Guid id, string title, Guid moduleId, ICollection<Content> contents)
    {
        Id = id;
        Title = title;
        ModuleId = moduleId;
        Contents = contents;
    }  

    public static Lesson Create(string title, Guid moduleId)
    {
        return new Lesson(Guid.NewGuid(), title, moduleId, new List<Content>());
    }

    public void AddContent(string title, string description, string contentType, string contentUrl)
    {
        Contents.Add(Content.Create(title, description, contentType, contentUrl, Id));
    }      
}
