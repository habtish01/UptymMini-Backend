

using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Security.UserRole
{
    public class UserRoleRepository : GRepository<Data.DbModels.SecuritySchema.ApplicationUserRole>, IUserRoleRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
