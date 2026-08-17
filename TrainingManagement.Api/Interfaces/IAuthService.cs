using TrainingManagement.Api.DTOs;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto dto);

    Task<LoginResponseDto?> LoginAsync(LoginDto dto);
}