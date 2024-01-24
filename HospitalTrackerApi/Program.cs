using HospitalTrackerApi.Data;
using HospitalTrackerApi.Data.Seeder;
using HospitalTrackerApi.Services;
using Microsoft.EntityFrameworkCore;

namespace HospitalTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalTrackerDbContext>(options =>
            {
                options.UseSqlite("Data Source=hospital.db");
            });

            builder.Services.AddScoped<ITrackerService, TrackerService>();

            builder.Services.AddHostedService<TimedHostedService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HospitalTrackerDbContext>();

                var seeder = new HospitalTrackerDbSeeder();

                seeder.Seed(context);
            }

            app.Run();
        }
    }
}