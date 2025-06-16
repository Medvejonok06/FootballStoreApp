using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballStoreApp.Models
{
    public class CategoryDetail
    {
        [Key]
        [ForeignKey("Category")] // Foreign Key = Primary Key
        public int Id { get; set; }

        [StringLength(100)]
        public string? ColorName { get; set; }

        [StringLength(10)]
        public string? ColorValue { get; set; }

        public Category? Category { get; set; }
    }
}
