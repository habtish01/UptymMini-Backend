using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.DbModels.ConfigurationSchema;

namespace Uptym.Data.DbModels.SecuritySchema
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
            RoleClaims = new HashSet<ApplicationRoleClaim>();
            UserRoles = new HashSet<ApplicationUserRole>(); 
            MenuRoles = new HashSet<MenuRole>();
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public override int Id { get; set; }
        public int RoleType { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
    }
}
