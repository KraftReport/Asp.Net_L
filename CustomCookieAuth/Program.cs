using CustomCookieAuth.Data;
using CustomCookieAuth.Entities;
using CustomCookieAuth.Filters;
using CustomCookieAuth.Middlewares;
using CustomCookieAuth.MinimalApis;
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

            builder.Services.AddScoped<CustomCookieAuthMiddleware>();

            builder.Services.AddScoped<ApplicationUserService>();

            builder.Services.AddScoped<ApplicationUserRepository>();

            builder.Services.AddScoped<ProductRepository>();

            builder.Services.AddScoped<ProductService>();

            builder.Services.AddScoped<LoggerService>();


            // Add security configuration
            builder.Services.AddAuthentication("CustomCookieAuthScheme").AddCookie("CustomCookieAuthScheme", options =>
            {
                options.Cookie.Name = "AuthCookie";
                options.LoginPath = "/api/Authentication/login"; 
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", p => p.RequireRole(ROLE.ADMIN.ToString()));
                options.AddPolicy("authenticated",p => p.RequireAuthenticatedUser());
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<LogFilter>();
            });


            builder.Services.AddLogging(options =>
            {
                options.AddConsole();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger for all environments (for troubleshooting purposes)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseHttpsRedirection();

            app.UseMiddleware<CustomCookieAuthMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.ProductMinimalApiRoutes();

            app.Run();

        }
    }
}
