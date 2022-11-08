using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO _userDao;

    public UserLogic(IUserDAO userDao)
    {
        _userDao = userDao;
    }


    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await _userDao.GetByUsernameAsync(dto.Username);
        if (existing!=null)
        {
            throw new Exception("Username already taken!");
        }

        ValidateUsername(dto);
        User toCreate = new User()
        {
            Username = dto.Username,
            Password = dto.Password
        };
        User created = await _userDao.CreateAsync(toCreate);
        return created;
    }

    public Task<User> GetByIdAsync(int id)
    {
        return _userDao.GetByIdAsync(id);
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        return _userDao.GetByUsernameAsync(username);
    }

    private void ValidateUsername(UserCreationDto dto)
    {
        if (dto.Username.Length < 3)
        {
            throw new Exception("Username must be at least 4 characters.");
        }
    }
}