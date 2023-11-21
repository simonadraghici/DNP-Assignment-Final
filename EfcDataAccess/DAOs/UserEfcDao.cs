using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User> LoginAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));
        user.Status = "active";
        if (existing != null)
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> LogoutAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));
        user.Status = "inactive";
        if (existing != null)
        {
            context.Users.Remove(existing);
            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower()) && u.Password.Equals(password)
        );
        return existing;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }

    public async Task<User?> GetById(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;
    }
}