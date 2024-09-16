using Microsoft.EntityFrameworkCore;
using ProjectFrameCRUD.Endpoint;
using ProjectFrameCRUD.Repository;
using ProjectFrameCRUD.Service; 
using ProjectFrameCRUD.Data;
using System.Text;
using ProjectFrameCRUD.Model;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Database configuration
        var connectionString = builder.Configuration.GetConnectionString("msp-dev");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Dependency injection
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<BookRepository>();
        builder.Services.AddScoped<IAuthService,AuthService>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<TokenRepository>();
        builder.Services.AddScoped<IJwtService,JWTService>();

        builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

        // Add Authorization policy
        builder.Services.AddAuthorization();
 

        // Swagger configuration
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // Enable Swagger in development mode
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root (i.e., https://localhost:<port>/)
            });
        }

 
        app.UseHttpsRedirection(); 
 
        app.BookRequestEndpoints();

        app.AuthEndpoints();
 
 

        app.Run();
    }
}
