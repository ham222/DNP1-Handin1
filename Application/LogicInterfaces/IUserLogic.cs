using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);

    public Task<User> GetByIdAsync(int id);

    public Task<User> GetByUsernameAsync(string username);

    Task<IEnumerable<User>> GetAllAsync();
}