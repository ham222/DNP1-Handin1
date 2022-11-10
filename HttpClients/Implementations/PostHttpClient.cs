using System.Net.Http.Json;
using System.Text.Json;
using Domain;
using Domain.DTOs;
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

    public async Task<Post> GetByTitle(string title)
    {
        ICollection<Post> all = await GetAllAsync();
        Post p = all.FirstOrDefault(post => post.Title.Equals(title))!;
        return p;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/posts", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}