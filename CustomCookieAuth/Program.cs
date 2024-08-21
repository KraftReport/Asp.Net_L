
using CustomCookieAuth.Data;
using CustomCookieAuth.Entities;
using CustomCookieAuth.Middlewares;
using CustomCookieAuth.Repositories;
using CustomCookieAuth.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add database configuration.
            builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("msp-dev"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("msp-dev"))
            ));

            builder.Services.AddTransient<CustomCookieAuthMiddleware>();
            builder.Services.AddTransient<ApplicationUserService>();
            builder.Services.AddTransient<ApplicationUserRepository>();
            builder.Services.AddTransient<ProductRepository>();
            builder.Services.AddTransient<ProductService>();


            // Add security configuration

            builder.Services.AddAuthentication("CustomCookieAuthScheme")
    .AddCookie("CustomCookieAuthScheme", options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/api/Authentication/login"; 
    });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", p => p.RequireRole(ROLE.ADMIN.ToString()));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<CustomCookieAuthMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
