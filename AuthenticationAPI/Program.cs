using AuthenticationAPI.BusinessLogic.Services;
using AuthenticationAPI.Data.Data;
using AuthenticationAPI.Data.Services;
using AuthenticationAPI.Domain.Services;
using AuthenticationAPI.Infrastructure.Rest;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AuthenticationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataRepository, DataRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IQRCodeService, QrCodeService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            builder.Services.AddSingleton<JsonSerializerOptions>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IRestClient, RestClient>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();
            
            app.Run();
        }
    }
}