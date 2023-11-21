namespace Domain.DTOs;

public class CommentCreationDto
{
    public string CreatorUsername { get; }
    public int PostId { get; }
    public string Content { get; }

    public CommentCreationDto(string creatorUsername, int postId, string content)
    {
        CreatorUsername = creatorUsername;
        PostId = postId;
        Content = content;
    }
}