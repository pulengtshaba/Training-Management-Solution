using TrainingManagement.Api.Models;

namespace TrainingManagement.Api.Services;

public interface ITokenService
{
    string CreateToken(User user);
}