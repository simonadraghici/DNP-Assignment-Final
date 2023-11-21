using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto dto);
    Task<User> LoginAsync(UserBasicDto dto);
    Task<User> LogoutAsync(UserBasicDto dto);
    Task<UserBasicDto> GetUser(string username);

}