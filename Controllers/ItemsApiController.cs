using FootballStoreApp.Dtos;
using FootballStoreApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FootballStoreApp.Models; // ✅ на всякий випадок, якщо ще буде використовуватись

namespace FootballStoreWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsApiController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsApiController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/ItemsApi
        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] ItemQueryParameters parameters)
        {
            var items = await _itemService.GetAllAsync(parameters);
            return Ok(items);
        }

        // GET: api/ItemsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _itemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/ItemsApi
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await _itemService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        // PUT: api/ItemsApi/5
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id)
        {
            // ❗ Якщо потрібно реалізувати оновлення — додамо метод UpdateAsync у сервіс
            return StatusCode(501, "Оновлення ще не реалізовано");
        }

        // DELETE: api/ItemsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var success = await _itemService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
