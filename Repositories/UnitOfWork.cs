using FootballStoreApp.Models;

namespace FootballStoreApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FootballStoreContext _context;

        public IItemRepository ItemRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(FootballStoreContext context)
        {
            _context = context;
            ItemRepository = new ItemRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
