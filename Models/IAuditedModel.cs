using System;

namespace FootballStoreApp.Models
{
    public interface IAuditedModel
    {
        int? CreatedByUserId { get; set; }
        DateTime CreatedDate { get; set; }
        int? LastModifiedUserId { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
