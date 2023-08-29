


using Uptym.Data.DataContext;
using Uptym.Repositories.Generics;

namespace Uptym.Repositories.Security.UserTransactionHistory
{
    public class UserTransactionHistoryRepository : GRepository<Data.DbModels.SecuritySchema.UserTransactionHistory>, IUserTransactionHistoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserTransactionHistoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
