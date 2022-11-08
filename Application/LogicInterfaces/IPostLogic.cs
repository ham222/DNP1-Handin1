using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);

    Task<IEnumerable<Post>> GetAllAsync();

    Task<Post> GetByIdAsync(int id);
}