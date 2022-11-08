using Application.DAOInterfaces;
using Domain;

namespace FileData.DAOs;

public class PostFileDao :IPostDAO
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;

        _context.Posts.Add(post);
        _context.SaveChanges();

        return Task.FromResult(post);
        
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Post>>(_context.Posts);
    }

    public Task<Post> GetByTitleAsync(string postTitle)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.Posts.FirstOrDefault(post => post.Id == id))!;
    }
}