
using System.Data;

namespace EduMaster.Domain.Entity;
public class Content
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ContentType { get; private set; } 
    public string ContentUrl { get; private set; } 
    public Guid LessonId { get; private set; }

    private Content(Guid id, string title, string description, string contentType, string contentUrl, Guid lessonId)
    {
        Id = id;
        Title = title;
        Description = description;
        ContentType = contentType;
        ContentUrl = contentUrl;
        LessonId = lessonId;
    }

    public static Content Create(string title, string description, string contentType, string contentUrl, Guid leassonId)
    {
        return new Content(Guid.NewGuid(), title, description, contentType, contentUrl, leassonId);
    }
}

