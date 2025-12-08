using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asteroidsbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemService ItemService => _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IItem>>> GetItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IItem>> GetItem(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<int>> CreateWeapon([FromBody] Weapon weapon)
        {
            var id = await _itemService.AddItemAsync(weapon);
            return CreatedAtAction(nameof(GetItem), new { id = id }, weapon);
        }

        [HttpPost("powerup")]
        public async Task<ActionResult<int>> CreatePowerUp([FromBody] PowerUp powerUp)
        {
            var id = await _itemService.AddItemAsync(powerUp);
            return CreatedAtAction(nameof(GetItem), new { id = id }, powerUp);
        }

        [HttpPut("weapon/{id}")]
        public async Task<IActionResult> UpdateWeapon(int id, [FromBody] Weapon weapon)
        {
            if (id != weapon.Id)
            {
                return BadRequest();
            }

            var result = await _itemService.UpdateItemAsync(weapon);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("powerup/{id}")]
        public async Task<IActionResult> UpdatePowerUp(int id, [FromBody] PowerUp powerUp)
        {
            if (id != powerUp.Id)
            {
                return BadRequest();
            }

            var result = await _itemService.UpdateItemAsync(powerUp);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
