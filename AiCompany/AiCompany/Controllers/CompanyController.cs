using Microsoft.AspNetCore.Mvc;
using AiCompany.Data.Models;
using AiCompany.Services.Interfaces;
using AiCompany.Services.Auth;
using AiCompany.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AiCompany.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AiCompany.API.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;
    private readonly AuthService _auth;

    public CompanyController(ICompanyService service, AuthService auth)
    {
        _service = service;
        _auth = auth;
    }

 


  
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentCompany()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var company = await _service.GetCompanyByEmailAsync(email!);
        if (company is null)
            return Unauthorized();

        return Ok(new
        {
            company.EnglishName,
            company.ArabicName,
            company.LogoPath
        });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _service.LoginAsync(dto.Email, dto.Password);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(new { token = result.Message });
    
  }
    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword([FromBody] SetPasswordDto dto)
    {
        var result = await _service.SetPasswordAsync(dto.Email, dto.Password);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] CompanyRegisterDto dto)
    {
        var logoPath = await SaveLogoAsync(dto.Logo);

        var company = new Company
        {
            ArabicName = dto.ArabicName,
            EnglishName = dto.EnglishName,
            Email = dto.Email,
            Phone = dto.Phone,
            Website = dto.Website,
            LogoPath = logoPath
        };

        var result = await _service.RegisterAsync(company);

        if (result == null)
            return Conflict("Email already used");

        return Ok(result);
    }

    private async Task<string?> SaveLogoAsync(IFormFile? logo)
    {
        if (logo == null || logo.Length == 0)
            return null;

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsFolder); 

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(logo.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await logo.CopyToAsync(stream);
        }

        return "/uploads/" + fileName; 
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
    {
        try
        {
            var result = await _service.VerifyOtpAsync(dto.Email, dto.OTP);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message); // أو: Ok("OTP Verified");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}

