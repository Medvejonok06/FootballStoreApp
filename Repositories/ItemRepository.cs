using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStoreApp.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(FootballStoreContext context) : base(context)
        {
        }

        public IQueryable<Item> GetAll()
        {
            return _context.Items.Include(i => i.Category).AsQueryable();
        }

        public async Task<List<Item>> GetLowStockItemsAsync(int threshold)
        {
            return await _context.Items
                .Where(i => i.Quantity <= threshold)
                .ToListAsync();
        }
    }
}
