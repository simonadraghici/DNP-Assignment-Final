using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IUserDao userDao;
    private readonly IPostDao postDao;

    public PostLogic(IUserDao userDao, IPostDao postDao)
    {
        this.userDao = userDao;
        this.postDao = postDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.OwnerUsername} was not found.");
        }

        Post post = new Post(dto.Title, dto.Body, user.UserName);
        ValidatePost(post);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        return postDao.GetAsync(searchPostParametersDto);
    }

    public async Task<Post?> GetById(int id)
    {
        Post? post = await postDao.GetById(id);
        if (post == null)
        {
            throw new Exception($"Todo with ID {post.Id} not found!");
        }

        return new Post(post.Title, post.Body, post.OwnerUsername);
    }

    private void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.Title))
        {
            throw new Exception("Title cannot be empty");
        }

        if (string.IsNullOrEmpty(post.Body))
        {
            throw new Exception("Body cannot be empty");
        }
    }
}