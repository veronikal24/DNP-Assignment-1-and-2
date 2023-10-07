using Application.DaoInterfaces;
using Domain_A1.Models;
using FileData.FileDaoImplem;

namespace FileData.DAOs;

public class PostFileDao: IPostDao
{
    private readonly FileContextA1 context;

    public PostFileDao(FileContextA1 context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.User.Id);
            id++;
        }
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
}