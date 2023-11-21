using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task Create(PostCreationDto dto);
    Task<IEnumerable<Post>> GetPosts(string? title = null);
    Task<Post> GetById(int id);
}