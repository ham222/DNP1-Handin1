using Application.DAOInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDAO
{

    private readonly EfcContext _efcContext;
    
    public PostEfcDao(EfcContext efcContext)
    {
        _efcContext = efcContext;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await _efcContext.Posts.AddAsync(post);
        await _efcContext.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        IEnumerable<Post> posts = _efcContext.Posts;
        return posts;
    }

    public async Task<Post> GetByTitleAsync(string postTitle)
    {
        Post? existing = await _efcContext.Posts.FirstOrDefaultAsync(p => p.Title.ToLower().Equals(postTitle.ToLower()));
        return existing;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? existing = await _efcContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        return existing;
    }
}