using Microsoft.AspNetCore.Mvc;
using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;

namespace MiniWallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var result = await _userService.RegisterAsync(dto);

        if (result == null)
        {
            return BadRequest(new
            {
                message = "Registration failed"
            });
        }

        return Ok(new
        {
            message = "User registered successfully",
            user = result
        });
       //return BadRequest();
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _userService.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password"
            });
        }

        return Ok(new
        {
            message = "Login successful",
            user = result
        });
    }
}