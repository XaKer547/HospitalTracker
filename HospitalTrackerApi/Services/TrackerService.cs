using HospitalTrackerApi.Data;
using HospitalTrackerApi.Helpers;
using HospitalTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalTrackerApi.Services
{
    public class TrackerService : ITrackerService
    {
        private readonly HospitalTrackerDbContext _context;
        public TrackerService(HospitalTrackerDbContext context)
        {
            _context = context;
        }

        public async Task ChangePositions()
        {
            var trackers = _context.Trackers.ToArray();

            var random = new Random();

            var trackedTime = DateTime.UtcNow;

            var totalRooms = _context.Rooms.Count();

            foreach (var tracker in trackers)
            {
                //2. покинет ли помещение?
                if (tracker.IsInsideRoom)
                {
                    var leaveRoom = random.NextBool();

                    if (leaveRoom)
                    {
                        tracker.IsInsideRoom = false;

                        tracker.LastTrackedTime = trackedTime;
                    }
                    continue;
                }

                //1. будет ли совершен трек
                if (random.NextBool())
                    continue;

                var roomNumber = random.Next(totalRooms);

                var room = _context.Rooms.Single(r => r.SecurityPointNumber == roomNumber);

                tracker.LastVisitedRoom = room;

                tracker.IsInsideRoom = true;

                tracker.LastTrackedTime = trackedTime;
            }

            _context.Trackers.UpdateRange(trackers);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<PersonTracker>> GetPersonLocations()
        {
            return await _context.Trackers.Select(t => new PersonTracker
            {
                PersonCode = t.Person.Id,
                PersonRole = t.Person.Role.Name,
                LastSecurityPointNumber = t.LastVisitedRoom.SecurityPointNumber,
                LastSecurityPointDirection = t.IsInsideRoom ? "in" : "out",
                LastSecurityPointTime = t.LastTrackedTime
            }).ToArrayAsync();
        }
    }
}