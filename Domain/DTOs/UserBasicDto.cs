namespace Domain.DTOs;

public class UserBasicDto
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string? Status { set; get; }

    public UserBasicDto(string UserName, string Password)
    {
        this.UserName = UserName;
        this.Password = Password;
    }
}