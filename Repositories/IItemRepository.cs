using FootballStoreApp.Models;

namespace FootballStoreApp.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<List<Item>> GetLowStockItemsAsync(int threshold);
        IQueryable<Item> GetAll();

    }
}
