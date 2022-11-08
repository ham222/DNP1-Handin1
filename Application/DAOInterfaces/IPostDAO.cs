using Domain;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);

    Task<IEnumerable<Post>> GetAllAsync();

    Task<Post> GetByTitleAsync(string postTitle);
}