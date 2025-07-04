using FootballStoreApp.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStoreApp.Interfaces
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetAllAsync(ItemQueryParameters parameters);
        Task<ItemDto?> GetByIdAsync(int id);
        Task<ItemDto> CreateAsync(CreateItemDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ItemDto?> UpdateAsync(int id, UpdateItemDto dto);
    }
}
