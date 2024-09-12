using Microsoft.EntityFrameworkCore;
using ProjectFrameCRUD.Controller;
using ProjectFrameCRUD.Data;
using ProjectFrameCRUD.Repository;
using ProjectFrameCRUD.Service;
using ProjectFrameCRUD;

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

        // Authorization and other services
        builder.Services.AddAuthorization();

        // Swagger configuration
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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
        app.UseAuthorization();
 
        app.BookRequestEndpoints(); 
 
 

        app.Run();
    }
}
