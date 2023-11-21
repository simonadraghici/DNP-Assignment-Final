using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User> LoginAsync(User user);
    Task<User> LogoutAsync(User user);
    Task<User?> GetUserAsync(string userName, string password);
    Task<User?> GetByUsernameAsync(string userName);
    Task<User?> GetById(int id);
}