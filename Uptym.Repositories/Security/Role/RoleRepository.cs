

using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Security.Role
{
    public class RoleRepository : GRepository<Data.DbModels.SecuritySchema.ApplicationRole>, IRoleRepository
    {
        private readonly AppDbContext _appDbContext;
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
