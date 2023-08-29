using System.Threading.Tasks;

namespace Uptym.Repositories.UOW
{
    public interface IUnitOfWork<T>
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
