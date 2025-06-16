using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballStoreApp.Models
{
    public class Category : FullAuditModel
    {
        [Required]
        [StringLength(InventoryModelsConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; } = null!;

        public List<Item> Items { get; set; } = new();

        public CategoryDetail? CategoryDetail { get; set; }

    }
}
