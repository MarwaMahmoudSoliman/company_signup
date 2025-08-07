using BCrypt.Net;

    using AiCompany.Data;
    using AiCompany.Data.Models;
    using AiCompany.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using BCrypt.Net;
    using global::AiCompany.Data.Models;
    using global::AiCompany.Services.Interfaces;
    using System;
using AiCompany.Data.AiCompany.Data;
using AiCompany.Dtos;
using Microsoft.AspNetCore.Mvc;
using AiCompany.Services.Auth;
using System.IO;

namespace AiCompany.Services.Implementations;

    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public CompanyService(AppDbContext context , ITokenService tokenService)
        {
            _context = context;
        _tokenService = tokenService;
    }
    public async Task<ResultDto> SetPasswordAsync(string email, string password)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Email == email && c.IsVerified);
        if (company == null)
            return ResultDto.Failure("Invalid email or not verified");

        company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        await _context.SaveChangesAsync();

        return ResultDto.Success("Password set successfully");
    }

    public async Task<Company?> GetCompanyByEmailAsync(string email)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
    }


    public async Task<Company?> RegisterAsync(Company company)
        {
            if (await _context.Companies.AnyAsync(c => c.Email == company.Email))
                return null;

            company.OTP = new Random().Next(100000, 999999).ToString();
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

         

            return company;
        }

       
    public async Task<ResultDto> VerifyOtpAsync(string email, string otp)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
        if (company == null || company.OTP != otp)
         
        return ResultDto.Failure("Invalid email or OTP");

        company.IsVerified = true;
        await _context.SaveChangesAsync();
        return ResultDto.Success("OTP Verified");
       
    }



    public async Task<ResultDto> LoginAsync(string email, string password)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Email == email);
        if (company == null || !BCrypt.Net.BCrypt.Verify(password, company.PasswordHash))
            return ResultDto.Failure("Invalid credentials");

        var token = _tokenService.GenerateJwtToken(company);
        return ResultDto.Success(token);
    }



 

}


