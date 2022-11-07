using System.Text.Json;
using Domain;

namespace FileData;

public class FileContext
{
    private const string Filepath = "data.json";
    private DataContainer? _container;

    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return _container!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _container!.Users;
        }
    }

    private void LoadData()
    {
        if (_container != null) return;
        
        if (!File.Exists(Filepath))
        {
            _container = new ()
            {
                Posts = new List<Post>(),
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(Filepath);
        _container = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_container, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(Filepath, serialized);
        _container = null;
    }
}