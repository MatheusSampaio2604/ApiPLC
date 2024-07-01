using Application.Services;
using Application.Services.Interfaces;
using Domain.Repository;
using Domain.Repository.Interface;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.WebHost.UseUrls("http://localhost:5000");

            builder.Services.AddControllers();

            builder.Services.AddSingleton<InterPlcService, PlcService>();
            builder.Services.AddSingleton<InterJsonService, JsonService>();
            builder.Services.AddSingleton<InterConfigureOptionsService, ConfigureOptionsService>(); 
#if DEBUG
            builder.Services.AddSingleton<IPlcListRepository, PlcsListRepositoy>(provider => new PlcsListRepositoy(Path.Combine(Directory.GetCurrentDirectory(), "..", "Domain", "Archives", "PlcsInUse.json")));
            builder.Services.AddSingleton<IPlcSettingsRepository, PlcSettingsRepository>(provider => new PlcSettingsRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "Domain", "Archives", "PlcSetting.json")));
            builder.Services.AddSingleton<IConfigureOptionsRepository, ConfigureOptionsRepository>(provider => new ConfigureOptionsRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "Domain", "Archives", "ConfigureOptions.json")));

#elif RELEASE
            builder.Services.AddSingleton<IPlcsJsonRepository, PlcsJsonRepositoy>(provider => new PlcsJsonRepositoy("/home/jetson/publish/publishdrv/PlcsInUse.json"));
            builder.Services.AddSingleton<IPlcSettingsJsonRepository, PlcSettingsJsonRepository>(provider => new PlcSettingsJsonRepository("/home/jetson/publish/publishdrv/PlcSetting.json"));
            builder.Services.AddSingleton<IConfigureOptionsRepository, ConfigureOptionsRepository>(provider => new ConfigureOptionsRepository("/home/jetson/publish/publishdrv/ConfigureOptions.json"));
#endif

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            //app.UseAuthorization();
            //app.UseAuthentication();

            app.MapControllers();

            // Configure o middleware CORS
            app.UseCors();

            app.Run();
        }
    }
}
