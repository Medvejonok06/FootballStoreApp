using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using FootballStoreApp.Models;

namespace FootballStoreApp.Models
{
    public class Item : FullAuditModel
    {
        [Required]
        [StringLength(InventoryModelsConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; } = null!;

        [StringLength(InventoryModelsConstants.DESCRIPTION_MAX_LENGTH)]
        public string? Description { get; set; }

        [StringLength(InventoryModelsConstants.NOTES_MAX_LENGTH)]
        public string? Notes { get; set; }

        [Range(InventoryModelsConstants.QUANTITY_MIN, InventoryModelsConstants.QUANTITY_MAX)]
        public int Quantity { get; set; }

        [Range((double)InventoryModelsConstants.PRICE_MIN, (double)InventoryModelsConstants.PRICE_MAX)]
        public decimal? PurchasePrice { get; set; }

        [Range((double)InventoryModelsConstants.PRICE_MIN, (double)InventoryModelsConstants.PRICE_MAX)]
        public decimal? CurrentOrFinalPrice { get; set; }

        public bool IsOnSale { get; set; }

        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
