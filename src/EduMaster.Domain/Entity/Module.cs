namespace EduMaster.Domain.Entity;
public class Module
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid SubjectId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Module(Guid id, string name, string description, Guid subjectId, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        SubjectId = subjectId;
        CreatedAt = createdAt;
    }

    public static Module Create(string name, string description, Guid subjectId)
    {
        return new Module(Guid.NewGuid(), name, description, subjectId, DateTime.UtcNow);
    }
        
}
