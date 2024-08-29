
using Microsoft.EntityFrameworkCore; 
using PlateDirectPaymentApi.Database;
using PlateDirectPaymentApi.DirectPaymentModule.Helper; 
using PlateDirectPaymentApi.DirectPaymentModule.Repository;
using PlateDirectPaymentApi.DirectPaymentModule.Service;

namespace PlateDirectPaymentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("msp-dev"));
            });

            builder.Services.AddScoped<MemberRepository>();
            builder.Services.AddScoped<MemberService>();
            builder.Services.AddScoped<CurrencyRepository>();
            builder.Services.AddScoped<CurrencyService>();
            builder.Services.AddScoped<TransactionRepository>();
            builder.Services.AddScoped<TransactionService>();
            builder.Services.AddScoped<LogHelper>();  
            builder.Services.AddScoped<ICurrencyService,CurrencyService>();
            builder.Services.AddScoped<IMemberService,MemberService>();
            builder.Services.AddScoped<ITransactionService,TransactionService>();

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

            app.Run();
        }
    }
}
