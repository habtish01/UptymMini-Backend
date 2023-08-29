
using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Configuration.ConfigurationAudit
{
    public class ConfigurationAuditRepository : GRepository<Data.DbModels.ConfigurationSchema.ConfigurationAudit>, IConfigurationAuditRepository
    {
        private readonly AppDbContext _appDbContext;
        public ConfigurationAuditRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
