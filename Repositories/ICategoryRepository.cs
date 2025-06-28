using FootballStoreApp.Models;

namespace FootballStoreApp.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<int> GetItemCountAsync(int categoryId);
            IQueryable<Category> Query();

    }
}
