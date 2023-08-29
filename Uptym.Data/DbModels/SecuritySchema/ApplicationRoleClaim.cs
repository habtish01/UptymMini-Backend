using Microsoft.AspNetCore.Identity;

namespace Uptym.Data.DbModels.SecuritySchema
{
    public class ApplicationRoleClaim: IdentityRoleClaim<int>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
