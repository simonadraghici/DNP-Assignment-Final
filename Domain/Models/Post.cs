namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string Body { get; set; }
    public User Owner { get; private set; }
    public string OwnerUsername { get; set; }

    public Post(string Title, string Body, string  ownerUsername)
    {
        this.Title = Title;
        this.Body = Body;
        this.OwnerUsername = ownerUsername;
    }
    private Post(){}
}