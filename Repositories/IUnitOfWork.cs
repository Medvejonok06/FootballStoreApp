using System;
using System.Threading.Tasks;

namespace FootballStoreApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository ItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task SaveAsync();
    }
}
