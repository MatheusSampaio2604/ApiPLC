
using Domain.Models;
using Application.Services;
using Domain.Repository.Interface;
using Domain.Repository;
using S7.Net.Types;
using Application.Services.Interfaces;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //builder.Services.Configure<PlcSettings>(builder.Configuration.GetSection("PlcSettings"));
            builder.Services.AddSingleton<InterPlcService, PlcService>();
            builder.Services.AddTransient<InterJsonService, JsonService>();

            builder.Services.AddSingleton<IPlcsJsonRepository, PlcsJsonRepositoy>(provider => new PlcsJsonRepositoy(Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure", "Archives", "PlcsConfigured.json")));
            builder.Services.AddSingleton<IPlcSettingsJsonRepository, PlcSettingsJsonRepository>(provider => new PlcSettingsJsonRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure", "Archives", "PlcSetting.json")));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
