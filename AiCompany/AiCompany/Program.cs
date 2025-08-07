
using AiCompany.Data.AiCompany.Data;
using AiCompany.Services.Auth;
using AiCompany.Services.Implementations;
using AiCompany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AiCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
          
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<AuthService>();


            builder.Services.AddControllers();

            var app = builder.Build();


   
            // Use the CORS policy
            app.UseCors("AllowAnyOrigin");



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();
     

            app.MapControllers();

            app.Run();
        }
    }
}
