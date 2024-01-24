using HospitalTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalTrackerApi.Controllers
{
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly ITrackerService _trackerService;
        public TrackerController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }


        [HttpGet("/PersonLocations")]
        public async Task<IActionResult> GetPersonLocationsAsync()
        {
            return Ok(await _trackerService.GetPersonLocations());
        }
    }
}
