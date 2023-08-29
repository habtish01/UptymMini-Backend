using Microsoft.AspNetCore.Identity;

namespace Uptym.Data.DbModels.SecuritySchema
{
    public class ApplicationUserToken: IdentityUserToken<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
