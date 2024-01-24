using HospitalTrackerApi.Models;

namespace HospitalTrackerApi.Services
{
    public interface ITrackerService
    {
        Task ChangePositions();
        Task<IReadOnlyCollection<PersonTracker>> GetPersonLocations();
    }
}
