using Application.Logic;
using Application.LogicInterfaces;
using Domain_A1.Models;
using Microsoft.AspNetCore.Mvc;
using Domain_A1.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this._userLogic = userLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await _userLogic.CreateAsync(dto);
            return Created($"/users/{user.UserName}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}