using AiCompany.Data.Models;

namespace AiCompany.Services.Auth;

public interface ITokenService
{
    string GenerateJwtToken(Company company);
}
