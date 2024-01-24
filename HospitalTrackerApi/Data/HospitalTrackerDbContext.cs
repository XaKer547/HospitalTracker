using Microsoft.EntityFrameworkCore;

namespace HospitalTrackerApi.Data
{
    public class HospitalTrackerDbContext : DbContext
    {
        public HospitalTrackerDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tracker> Trackers { get; set; }
    }
}