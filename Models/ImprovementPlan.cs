using System.ComponentModel.DataAnnotations;

namespace FootballStoreApp.Models
{
    public class ImprovementPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PlanName { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
