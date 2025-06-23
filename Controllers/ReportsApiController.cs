using FootballStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballStoreWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsApiController : ControllerBase
    {
        private readonly FootballStoreContext _context;

        public ReportsApiController(FootballStoreContext context)
        {
            _context = context;
        }

        // 📊 Загальна статистика
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var totalItems = await _context.Items.CountAsync(i => i.IsActive && !i.IsDeleted);
            var totalQuantity = await _context.Items.SumAsync(i => i.Quantity);
            var totalValue = await _context.Items.SumAsync(i => i.Quantity * i.CurrentOrFinalPrice ?? 0);

            return Ok(new
            {
                totalItems,
                totalQuantity,
                totalValue
            });
        }

        // 📦 Кількість товарів по категоріях
        [HttpGet("category-count")]
        public async Task<IActionResult> GetCategoryItemCount()
        {
            var data = await _context.Items
                .Where(i => i.IsActive && !i.IsDeleted)
                .GroupBy(i => i.Category!.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(data);
        }

        // ⚠️ Товари з низьким залишком
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockItems()
        {
            var items = await _context.Items
                .Where(i => i.Quantity <= 5 && i.IsActive && !i.IsDeleted)
                .Select(i => new
                {
                    i.Id,
                    i.Name,
                    i.Quantity
                })
                .ToListAsync();

            return Ok(items);
        }
    }
}
