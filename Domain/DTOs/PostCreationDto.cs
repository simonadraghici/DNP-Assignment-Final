namespace Domain.DTOs;

public class PostCreationDto
{
    public string OwnerUsername { get; }
    public string Title { get; }
    public string Body { get; }

    public PostCreationDto(string ownerUsername, string title, string body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}