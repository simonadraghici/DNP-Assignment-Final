using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post?> GetById(int id)
    {
        Post? found = await context.Posts.SingleOrDefaultAsync(post => post.Id == id);
        return found;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto)
    {
        IQueryable<Post> query = context.Posts.Include(todo => todo.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(dto.OwnerUsername))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(todo =>
                todo.Owner.UserName.ToLower().Equals(dto.OwnerUsername.ToLower()));
        }
    
        if (dto.OwnerId != null)
        {
            query = query.Where(t => t.Owner.Id == dto.OwnerId);
        }
    
        if (!string.IsNullOrEmpty(dto.Title))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(dto.Title.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }
}