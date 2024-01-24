using HospitalTrackerApi.Helpers;

namespace HospitalTrackerApi.Data.Seeder
{
    public partial class HospitalTrackerDbSeeder
    {
        public async Task SeedTrackers(HospitalTrackerDbContext context)
        {
            if (context.Trackers.Any())
                return;

            var persons = context.Persons.ToArray();

            var random = new Random();

            var minTrackedTime = DateTime.Now;
            foreach (var person in persons)
            {
                var roomNumber = random.Next(TotalRooms - 1);

                var room = context.Rooms.Single(r => r.SecurityPointNumber == roomNumber);

                context.Trackers.Add(new Tracker()
                {
                    Person = person,
                    LastVisitedRoom = room,
                    IsInsideRoom = random.NextBool(),
                    LastTrackedTime = minTrackedTime,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
