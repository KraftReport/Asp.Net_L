
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaApiWithRedis.Database;
using PizzaApiWithRedis.Pizza.Repository;
using PizzaApiWithRedis.Pizza.Service;
using PizzaApiWithRedis.Security.Service;

namespace PizzaApiWithRedis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database to the container

            var connectionString = builder.Configuration.GetConnectionString("msp-dev");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddScoped<PizzaRepository>();
            builder.Services.AddScoped<IPizzaService, PizzaService>();
            builder.Services.AddScoped<ICacheManagerService, CacheManager>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    o =>
                    {
                        o.RequireHttpsMetadata = false;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = builder.Configuration["JwtIssuer"],
                            ValidAudience = builder.Configuration["JwtAudience"],
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSecret"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            // Add services to the container.

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

       


            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.Run();
        }
    }
}
