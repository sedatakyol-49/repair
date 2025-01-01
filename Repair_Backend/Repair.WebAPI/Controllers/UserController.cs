using Microsoft.AspNetCore.Mvc;
using Repair.BusinessLogic.UserBL;
using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.DTOs.Repair;
using Repair.Infrastructure.DTOs.User;

namespace Repair.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetRepairs()
    {
        var users = await _userService.GetUsersWithDetailsAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto dto)
    {
        try
        {
            var user = await _userService.AddUserAsync(dto);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(Guid id, UpdateUserDto user)
    {
        if (id != user.Id) return BadRequest();

        var existingUser = await _userService.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        await _userService.UpdateUserAsync(user);

        return NoContent();
    }
}

