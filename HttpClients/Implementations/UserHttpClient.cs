using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient:IUserService
{
    private readonly HttpClient _client;

    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<User> Create(UserCreationDto dto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/users", dto);
        string result = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result,new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
    
    public async Task<ICollection<User>> GetAllAsync()
    {
        HttpResponseMessage responseMessage = await _client.GetAsync("/users");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}