using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<Post?> GetById(int id);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto);
}