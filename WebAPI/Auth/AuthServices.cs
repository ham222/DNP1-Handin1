using System.Text.Json;
using Application.LogicInterfaces;
using Domain;

namespace WebAPI.Auth;

public class AuthServices : IAuthService
{
    private readonly IUserLogic _userLogic;
    private IEnumerable<User> users;

    public AuthServices(IUserLogic logic)
    {
        _userLogic = logic;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        users = await _userLogic.GetAllAsync();
        
        
        User? existingUser = users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        throw new NotImplementedException();
    }
}