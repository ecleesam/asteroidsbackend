using asteroidsbackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asteroidsbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly RandomDataService _randomDataService;

        public DataController(RandomDataService randomDataService)
        {
            _randomDataService = randomDataService;
        }

        [HttpPost("generate-sample")]
        public async Task<IActionResult> GenerateSampleData()
        {
            await _randomDataService.GenerateSampleDatasetAsync();
            return Ok(new { message = "Sample data generated successfully" });
        }
    }
}
