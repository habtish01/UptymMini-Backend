
using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Configuration.Configuration
{
    public class UserPreferenceRepository : GRepository<Data.DbModels.ConfigurationSchema.UserPreference>, IUserPreferenceRepository
    {

        private readonly AppDbContext _appDbContext;
        public UserPreferenceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
