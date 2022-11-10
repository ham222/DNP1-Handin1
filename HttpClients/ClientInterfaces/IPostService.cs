using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<ICollection<Post>> GetAllAsync();

    Task<Post> GetById(int id);
    Task<Post> GetByTitle(string title);

    Task CreateAsync(PostCreationDto dto);
}