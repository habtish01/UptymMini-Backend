using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Subscription.CustomerType
{
    public class CustomerTypeRepository : GRepository<Data.DbModels.SubscriptionSchema.CustomerType>, ICustomerTypeRepository
    {
        private readonly AppDbContext _appDbContext;
        public CustomerTypeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
