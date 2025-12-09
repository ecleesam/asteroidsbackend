using System.Security.Claims;
using asteroidsbackend.Data;
using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asteroidsbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("buy/{itemId}")]
        public async Task<IActionResult> BuyItem(int itemId)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var user = await _context.Users.FindAsync(userId);
            var item = await _context.Items.FindAsync(itemId);

            if (user == null || item == null) return NotFound("User or Item not found");

            if (user.Credits < item.Value)
                return BadRequest("Insufficient credits");

            user.Credits -= item.Value;
            // Logic to add item to user's inventory would go here
            // For now, we just deduct credits
            
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Bought {item.Name} for {item.Value} credits", remainingCredits = user.Credits });
        }

        [HttpGet("credits")]
        public async Task<IActionResult> GetCredits()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            return Ok(new { credits = user.Credits });
        }

        [HttpPost("earn-credits")]
        public async Task<IActionResult> EarnCredits([FromBody] EarnCreditsDto request)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound("User not found");

            if (request.Amount <= 0) return BadRequest("Amount must be positive");

            user.Credits += request.Amount;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Earned {request.Amount} credits", totalCredits = user.Credits });
        }
    }

    public class EarnCreditsDto
    {
        public int Amount { get; set; }
    }
}
