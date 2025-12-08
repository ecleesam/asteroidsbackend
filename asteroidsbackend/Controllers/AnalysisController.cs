using asteroidsbackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asteroidsbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnalysisController : ControllerBase
    {
        private readonly AnalysisService _analysisService;

        public AnalysisController(AnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoryCounts()
        {
            var counts = await _analysisService.GetCategoryCountsAsync();
            var result = counts.ToDictionary(k => k.Key.ToString(), v => v.Value);
            return Ok(result);
        }

        [HttpGet("top-weapons")]
        public async Task<IActionResult> GetTopWeapons()
        {
            var weapons = await _analysisService.GetTop5WeaponsAsync();
            return Ok(weapons);
        }
    }
}
