﻿using Microsoft.AspNetCore.Identity;

namespace Uptym.Data.DbModels.SecuritySchema
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
