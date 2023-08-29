using Microsoft.AspNetCore.Identity;
namespace Uptym.Data.DbModels.SecuritySchema
{
    public class ApplicationUserLogin: IdentityUserLogin<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
