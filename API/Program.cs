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

            //builder.Services.Configure<PlcSettings>(builder.Configuration.GetSection("PlcSettings"));
            builder.Services.AddSingleton<InterPlcService, PlcService>();
            builder.Services.AddTransient<InterJsonService, JsonService>();

#if DEBUG
            builder.Services.AddSingleton<IPlcsJsonRepository, PlcsJsonRepositoy>(provider => new PlcsJsonRepositoy(Path.Combine(Directory.GetCurrentDirectory(), "..", "Domain", "Archives", "PlcsInUse.json")));
            builder.Services.AddSingleton<IPlcSettingsJsonRepository, PlcSettingsJsonRepository>(provider => new PlcSettingsJsonRepository(Path.Combine(Directory.GetCurrentDirectory(), "..", "Domain", "Archives", "PlcSetting.json")));
#elif RELEASE
            builder.Services.AddSingleton<IPlcsJsonRepository, PlcsJsonRepositoy>(provider => new PlcsJsonRepositoy("/home/jetson/publish/publishdrv/PlcsInUse.json"));
            builder.Services.AddSingleton<IPlcSettingsJsonRepository, PlcSettingsJsonRepository>(provider => new PlcSettingsJsonRepository("/home/jetson/publish/publishdrv/PlcSetting.json"));
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
