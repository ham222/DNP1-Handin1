using System.Text.Json;
using Domain;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient:IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        HttpResponseMessage responseMessage = await _client.GetAsync("/posts");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> GetById( int id)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/posts/{id}");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }
}