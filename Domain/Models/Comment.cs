namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public User Creator { get; set; }
    public Post Post { get; set; }
    public string Content { get; set; }

    public Comment(User creator, Post post, string content)
    {
        Creator = creator;
        Post = post;
        Content = content;
    }
}