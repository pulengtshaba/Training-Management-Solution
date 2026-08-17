namespace TrainingManagement.Api.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string Username { get; set; } = "";

    public string Role { get; set; } = "";
}