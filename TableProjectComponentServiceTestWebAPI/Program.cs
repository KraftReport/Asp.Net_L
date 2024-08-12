
using System;
using TableProjectComponentServiceTestWebAPI.Audio;
using TableProjectComponentServiceTestWebAPI.CustomException;
using TableProjectComponentServiceTestWebAPI.QrCode;
using TableProjectComponentServiceTestWebAPI.Ticket;

namespace TableProjectComponentServiceTestWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var configuration = builder.Configuration;

            builder.Services.AddSingleton<TicketRepository>();

            builder.Services.AddScoped<AudioService>();

            builder.Services.AddScoped<QrCodeService>();
            
            builder.Services.AddScoped<AudioDao>();

            builder.Services.AddScoped<Logger>();

            builder.Services.AddTransient<Handler>();

            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

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

            app.UseMiddleware<Handler>();

            app.Run();
        }
    }
}
