using FootballStoreApp.Dtos;
using FootballStoreApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballStoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET api/items
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ItemQueryParameters queryParameters)
        {
            var items = await _itemService.GetAllAsync(queryParameters);
            return Ok(items);
        }

        // GET api/items/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
        {
            var created = await _itemService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDto dto)
        {
            var updated = await _itemService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE api/items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _itemService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
