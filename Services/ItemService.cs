using FootballStoreApp.Dtos;
using FootballStoreApp.Interfaces;
using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using FootballStoreApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStoreApp.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ItemDto>> GetAllAsync(ItemQueryParameters parameters)
        {
            var query = _unitOfWork.ItemRepository
                .GetAll()
                .Where(i => !i.IsDeleted && i.IsActive);

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(i => i.Name.ToLower().Contains(parameters.Name.ToLower()));
            }

            if (parameters.MinPrice.HasValue)
            {
                query = query.Where(i => i.CurrentOrFinalPrice >= parameters.MinPrice);
            }

            if (parameters.MaxPrice.HasValue)
            {
                query = query.Where(i => i.CurrentOrFinalPrice <= parameters.MaxPrice);
            }

            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                switch (parameters.SortBy.ToLower())
                {
                    case "name":
                        query = parameters.Descending ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                        break;
                    case "price":
                        query = parameters.Descending ? query.OrderByDescending(i => i.CurrentOrFinalPrice) : query.OrderBy(i => i.CurrentOrFinalPrice);
                        break;
                    case "quantity":
                        query = parameters.Descending ? query.OrderByDescending(i => i.Quantity) : query.OrderBy(i => i.Quantity);
                        break;
                }
            }

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
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            if (item == null || item.IsDeleted || !item.IsActive)
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
                CategoryId = dto.CategoryId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.ItemRepository.AddAsync(item);
            await _unitOfWork.SaveAsync();

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

        public async Task<ItemDto?> UpdateAsync(int id, UpdateItemDto dto)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            if (item == null || item.IsDeleted || !item.IsActive)
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
            item.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();

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
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            if (item == null || item.IsDeleted)
                return false;

            item.IsDeleted = true;
            item.IsActive = false;
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
