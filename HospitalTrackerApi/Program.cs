using HospitalTrackerApi.Data;
using HospitalTrackerApi.Data.Seeder;
using HospitalTrackerApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HospitalTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalTrackerDbContext>(options =>
            {
                options.UseSqlite("Data Source=C:\\Users\\Public\\hospital.db");
            });

            builder.Services.AddScoped<ITrackerService, TrackerService>();

            builder.Services.AddHostedService<TimedHostedService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HospitalTrackerDbContext>();

                var seeder = new HospitalTrackerDbSeeder();

                seeder.Seed(context)
                    .GetAwaiter()
                    .GetResult();
            }

            app.Run();
        }
    }
}