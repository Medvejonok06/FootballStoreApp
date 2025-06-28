using FootballStoreApp.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStoreApp.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
