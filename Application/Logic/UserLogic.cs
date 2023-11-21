using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("This username is already taken, please choose another one");
        
        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Status = "active"
        };
        User created = await userDao.CreateAsync(toCreate);
        return created;
    }

    public async Task<User> LoginAsync(UserBasicDto dto)
    {
        User? existing = await userDao.GetUserAsync(dto.UserName, dto.Password);
        if (existing == null)
            throw new Exception("The account with the given username and password does not exist. Please try again.");
        User loggedin = await userDao.LoginAsync(existing);
        return loggedin;
    }

    public async Task<User> LogoutAsync(UserBasicDto dto)
    {
        User? existing = await userDao.GetUserAsync(dto.UserName, dto.Password);
        User loggedout = await userDao.LogoutAsync(existing);
        return loggedout;
    }

    public async Task<UserBasicDto> GetUser(string username)
    {
        User? user = await userDao.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception($"User with given username doesn't exist");
        }

        return new UserBasicDto(user.UserName, user.Password);
    }

    private static void ValidateData(UserCreationDto userCreationDto)
    {
        string userName = userCreationDto.UserName;
        string password = userCreationDto.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if (password.Length < 9)
            throw new Exception("Password must be at least 9 characters!");

        if (password.Length > 15)
            throw new Exception("Password must be less than 16 characters!");
    }
}