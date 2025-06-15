using System;
using System.ComponentModel.DataAnnotations;

namespace FootballStoreApp.Models
{
    public abstract class FullAuditModel : IIdentityModel, IAuditedModel, IActivatableModel
    {
        [Key]
        public int Id { get; set; }

        public int? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
