using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao: IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User> LoginAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));
        user.Status = "active";
        if (existing != null)
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User> LogoutAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));
        user.Status = "unactive";
        if (existing != null && existing.Status.Equals("active"))
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User?> GetUserAsync(string userName, string password)
    {
        User user = new User();
        user.UserName = userName;
        user.Password = password;
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(password));
        return Task.FromResult(existing);
    }

    public Task<User?> GetByUsernameAsync(string? userName)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }
}