

using Application.Logic;
using Application.LogicInterfaces;
using Domain_A1.Models;
using Microsoft.AspNetCore.Mvc;
using Domain_A1.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : Controller
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        this._postLogic= postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post created = await _postLogic.CreateAsync(dto);
            return Created($"/post/{created.Title}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


}