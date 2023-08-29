using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.DbModels.SecuritySchema;

namespace Uptym.Data.DbModels.ConfigurationSchema
{
    [Table("MenuRoles", Schema = "Configuration")]
    public class MenuRole
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ApplicationRole Role { get; set; }
                 
    }
}
