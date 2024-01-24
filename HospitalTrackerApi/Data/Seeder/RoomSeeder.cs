namespace HospitalTrackerApi.Data.Seeder
{
    public partial class HospitalTrackerDbSeeder
    {
        private const int TotalRooms = 23;
        public async Task SeedRoomsAsync(HospitalTrackerDbContext context)
        {
            if (context.Rooms.Any())
                return;

            for (int i = 0; i < TotalRooms; i++)
            {
                context.Rooms.Add(new Room()
                {
                    SecurityPointNumber = i
                });
            }

            await context.SaveChangesAsync();
        }
    }
}