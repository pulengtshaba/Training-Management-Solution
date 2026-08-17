using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainingManagement.Api.Data;
using TrainingManagement.Api.DTOs;

namespace TrainingManagement.Api.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext context,
    ITokenService tokenService)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
        _tokenService = tokenService;

    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        // 1. Check whether username already exists
        var usernameExists = await _context.Users
            .AnyAsync(u => u.Username == dto.Username);

        if (usernameExists)
        {
            return false;
        }

        // 2. Check whether email already exists
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == dto.Email);

        if (emailExists)
        {
            return false;
        }

        // 3. Create the User entity
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Role = "Employee",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // 4. Hash the password
        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            dto.Password);

        // 5. Add the user to EF Core
        _context.Users.Add(user);

        // 6. Save the user to SQL Server
        await _context.SaveChangesAsync();

        // 7. Registration succeeded
        return true;
    }
    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
            return null;

        if (!user.IsActive)
            return null;

        var passwordHasher = new PasswordHasher<User>();

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        var token = _tokenService.CreateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role
        };
    }
}