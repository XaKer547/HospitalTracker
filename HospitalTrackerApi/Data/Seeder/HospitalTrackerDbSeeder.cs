global using HospitalTrackerApi.Data.Entities;

namespace HospitalTrackerApi.Data.Seeder
{
    public partial class HospitalTrackerDbSeeder
    {
        public async Task Seed(HospitalTrackerDbContext context)
        {
            await SeedRolesAsync(context);

            await SeedRoomsAsync(context);

            await SeedPersonsAsync(context);

            await SeedTrackers(context);
        }
    }
}