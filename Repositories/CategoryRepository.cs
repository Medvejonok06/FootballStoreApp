using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStoreApp.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FootballStoreContext context) : base(context)
        {
        }

        public async Task<int> GetItemCountAsync(int categoryId)
        {
            return await _context.Items.CountAsync(i => i.CategoryId == categoryId);
        }

        public IQueryable<Category> Query()
        {
            return _context.Categories.Include(c => c.Items);
        }
    }
}
