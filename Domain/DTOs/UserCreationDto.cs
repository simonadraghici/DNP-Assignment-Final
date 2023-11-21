namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string Password { get; }

    public UserCreationDto(string UserName, string Password)
    {
        this.UserName = UserName;
        this.Password = Password;
    }
}