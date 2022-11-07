using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO _postDao;
    private readonly IUserDAO _userDao;

    public PostLogic(IPostDAO postDao, IUserDAO userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await _userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} not found");
        }

        Post post = new Post(user, dto.Title, dto.Body);
        ValidatePost(post);
        Post created = await _postDao.CreateAsync(post);
        return created;
    }
    
    private void ValidatePost(Post dto)
    {
        if ((string.IsNullOrEmpty(dto.Title))||(string.IsNullOrEmpty(dto.Body))) throw new Exception("Title and body must contain characters.");
        // other validation stuff
    }
}