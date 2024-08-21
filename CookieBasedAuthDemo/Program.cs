using CookieBasedAuthDemo.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CookieBasedAuthDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // db services

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("msp-dev"));
            });

            // security configuration

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "authCookie";
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                    options.AccessDeniedPath = "/api/auth/accessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", ao =>
                {
                    ao.RequireClaim(ClaimTypes.Role, "Admin");
                });

                options.AddPolicy("UserAndAdmin", ua =>
                {
                    ua.RequireAuthenticatedUser();
                });
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

            app.UseAuthorization();

            app.MapControllers();

            /*using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Add test users
                context.Users.Add(new AppUser { UserName = "admin", Password = "password", Role = "Admin" });
                context.Users.Add(new AppUser { UserName = "user", Password = "password", Role = "User" });

                // Add test products
                context.Products.Add(new Product.Product { Name = "Product1", Price = 1000 });
                context.Products.Add(new Product.Product { Name = "Product2", Price = 2000 });

                context.SaveChanges();
            }*/

            app.Run();
        }
    }
}
