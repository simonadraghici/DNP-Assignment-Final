using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    [HttpPost] 
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)//the ActionResult is an HTTP response type, which contains various extra data, other than what we provide
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.UserName}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet] //we mark the method with [HttpGet] so that GET requests to this controller end here
    //the return value is the IEnumerable<User> wrapped in an HTTP response message
    public async Task<ActionResult<User>> GetUserAsync([FromQuery] string username)//the argument is marked as [FromQuery] to indicate that this argument should be extracted from the query parameters of the URI
    {
        try
        {
            UserBasicDto userBasicDto = await userLogic.GetUser(username);
            User user = new User
            {
                UserName = userBasicDto.UserName,
                Password = userBasicDto.Password,
                Status = userBasicDto.Status
            };
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}