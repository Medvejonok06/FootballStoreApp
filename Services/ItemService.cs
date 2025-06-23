using FootballStoreApp.Dtos;
using FootballStoreApp.Interfaces;
using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballStoreApp.Services
{
    public class ItemService : IItemService
    {
        private readonly FootballStoreContext _context;

        public ItemService(FootballStoreContext context)
        {
            _context = context;
        }

        public async Task<List<ItemDto>> GetAllAsync(ItemQueryParameters parameters)
        {
            var query = _context.Items
                .Where(i => i.IsActive && !i.IsDeleted);

            if (!string.IsNullOrEmpty(parameters.Name))
                query = query.Where(i => i.Name.ToLower().Contains(parameters.Name.ToLower()));

            if (parameters.MinPrice.HasValue)
                query = query.Where(i => i.CurrentOrFinalPrice >= parameters.MinPrice);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(i => i.CurrentOrFinalPrice <= parameters.MaxPrice);

            query = query
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            var items = await query.ToListAsync();

            return items.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Quantity = i.Quantity,
                CurrentOrFinalPrice = i.CurrentOrFinalPrice,
                CategoryId = i.CategoryId
            }).ToList();
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null || item.IsDeleted)
                return null;

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                CurrentOrFinalPrice = item.CurrentOrFinalPrice,
                CategoryId = item.CategoryId
            };
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto dto)
        {
            var item = new Item
            {
                Name = dto.Name,
                Description = dto.Description,
                Notes = dto.Notes,
                Quantity = dto.Quantity,
                PurchasePrice = dto.PurchasePrice,
                CurrentOrFinalPrice = dto.CurrentOrFinalPrice,
                IsOnSale = dto.IsOnSale,
                PurchasedDate = dto.PurchasedDate?.ToUniversalTime(),
                SoldDate = dto.SoldDate?.ToUniversalTime(),
                CategoryId = dto.CategoryId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                CurrentOrFinalPrice = item.CurrentOrFinalPrice,
                CategoryId = item.CategoryId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return false;

            _context.Items.Remove(item); // спрацює soft-delete через SaveChanges override
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ItemDto?> UpdateAsync(int id, UpdateItemDto dto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return null;

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Notes = dto.Notes;
            item.Quantity = dto.Quantity;
            item.PurchasePrice = dto.PurchasePrice;
            item.CurrentOrFinalPrice = dto.CurrentOrFinalPrice;
            item.IsOnSale = dto.IsOnSale;
            item.PurchasedDate = dto.PurchasedDate?.ToUniversalTime();
            item.SoldDate = dto.SoldDate?.ToUniversalTime();
            item.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                CurrentOrFinalPrice = item.CurrentOrFinalPrice,
                CategoryId = item.CategoryId
            };
        }
    }
}
