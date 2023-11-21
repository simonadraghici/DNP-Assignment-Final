using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly IUserDao _userDao;
    private readonly IPostDao _postDao;
    private readonly ICommentDao _commentDao;

    public CommentLogic(IUserDao userDao, IPostDao postDao, ICommentDao commentDao)
    {
        _userDao = userDao;
        _postDao = postDao;
        _commentDao = commentDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        User? user = await _userDao.GetByUsernameAsync(dto.CreatorUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.CreatorUsername} was not found");
        }
        Post? post = await _postDao.GetById(dto.PostId);
        if (post == null)
        {
            throw new Exception($"Post with id {dto.PostId} was not found");
        }

        Comment comment = new Comment(user, post, dto.Content);
        ValidateComment(comment);
        Comment created = await _commentDao.CreateAsync(comment);
        return created;
    }

    private void ValidateComment(Comment comment)
    {
        if (string.IsNullOrEmpty(comment.Content)) throw new Exception("Comment cannot be empty.");
    }
}