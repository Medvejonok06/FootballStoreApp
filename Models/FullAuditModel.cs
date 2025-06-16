using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FootballStoreApp.Models
{
    public abstract class FullAuditModel : IIdentityModel, IAuditedModel, IActivatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public int? CreatedByUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public int? LastModifiedUserId { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
