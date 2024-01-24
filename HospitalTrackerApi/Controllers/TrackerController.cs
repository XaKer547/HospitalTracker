using HospitalTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalTrackerApi.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class TrackerController : ControllerBase
    {
        private readonly ITrackerService _trackerService;
        public TrackerController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }


        [HttpGet("PersonLocation")]
        public async Task<IActionResult> GetPersonLocationsAsync()
        {
            return Ok(await _trackerService.GetPersonLocations());
        }
    }
}
