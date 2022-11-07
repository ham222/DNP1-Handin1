using Domain;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);

    Task<Post> GetByTitleAsync(string postTitle);
}