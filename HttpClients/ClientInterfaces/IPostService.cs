using Domain;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<ICollection<Post>> GetAllAsync();
}