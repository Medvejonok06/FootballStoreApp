using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FootballStoreApp.Dtos;

namespace FootballStoreApp.Models
{
    public abstract class FullAuditModel : IIdentityModel, IAuditedModel, IActivatableModel
    {
        [Key]
        public int Id { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? LastModifiedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;  // ✅ виправлено: ініціалізовано

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
