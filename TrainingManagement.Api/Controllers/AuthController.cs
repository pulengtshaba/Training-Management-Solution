using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Api.DTOs;
using TrainingManagement.Api.Models.Common;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = "Username or email already exists.",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "User registered successfully.",
            Data = null
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid username or password.",
                Data = null
            });
        }

        return Ok(new ApiResponse<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful.",
            Data = result
        });
    }
}