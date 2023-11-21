using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext _fileContext;

    public CommentFileDao(FileContext fileContext)
    {
        _fileContext = fileContext;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        int commentId = 1;
        if (_fileContext.Comments.Any())
        {
            commentId = _fileContext.Comments.Max(c => c.Id);
            commentId++;
        }

        comment.Id = commentId;
        _fileContext.Comments.Add(comment);
        _fileContext.SaveChanges();
        return Task.FromResult(comment);
    }
}