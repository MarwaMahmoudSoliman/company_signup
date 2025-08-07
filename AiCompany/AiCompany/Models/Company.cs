namespace AiCompany.Data.Models;

public class Company
{
    public int Id { get; set; }
  
    public string ArabicName { get; set; } = null!;
    public string EnglishName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public string? LogoPath { get; set; }

    public string? OTP { get; set; }
    public bool IsVerified { get; set; } = false;

    public string? PasswordHash { get; set; }
}
