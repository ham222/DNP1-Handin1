using Application.DAOInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDAO
{

    private readonly PostContext _postContext;
    
    public PostEfcDao(PostContext postContext)
    {
        _postContext = postContext;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await _postContext.Posts.AddAsync(post);
        await _postContext.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        IEnumerable<Post> posts = _postContext.Posts;
        return posts;
    }

    public async Task<Post> GetByTitleAsync(string postTitle)
    {
        Post? existing = await _postContext.Posts.FirstOrDefaultAsync(p => p.Title.ToLower().Equals(postTitle.ToLower()));
        return existing;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? existing = await _postContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        return existing;
    }
}