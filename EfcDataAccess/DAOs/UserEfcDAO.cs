using Application.DAOInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDAO
{
    private readonly PostContext _context;

    public UserEfcDao(PostContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(u =>
            u.Username.ToLower().Equals(username.ToLower())
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        User? existing = _context.Users.FirstOrDefault(u =>
            u.Id == userId
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_context.Users);
    }
}