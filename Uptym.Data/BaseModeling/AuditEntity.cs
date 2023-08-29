using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.DbModels.SecuritySchema;

namespace Uptym.Data.BaseModeling
{
    public class AuditEntity
    {
        public int Id { get; set; }
        public DateTime DateOfAction { get; set; }
        public string Action { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public int CreatedBy { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
