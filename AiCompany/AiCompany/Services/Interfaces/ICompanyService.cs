using AiCompany.Data.Models;
using AiCompany.Dtos;

namespace AiCompany.Services.Interfaces
{

    public interface ICompanyService
    {
        Task<Company?> RegisterAsync(Company company);
        Task<ResultDto> VerifyOtpAsync(string email, string otp);
        Task<ResultDto> SetPasswordAsync(string email, string password);

        Task<ResultDto> LoginAsync(string email, string password);
        Task<Company?> GetCompanyByEmailAsync(string email);

    }
}
