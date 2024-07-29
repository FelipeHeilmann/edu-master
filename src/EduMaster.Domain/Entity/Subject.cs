namespace EduMaster.Domain.Entity;
public class Subject
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Subject(Guid id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }        

    public static Subject Create(string name)
    {
        return new Subject(Guid.NewGuid(), name, DateTime.UtcNow);
    }
}
