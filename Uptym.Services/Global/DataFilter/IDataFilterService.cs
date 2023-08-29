using System.Linq;

namespace Uptym.Services.Global.DataFilter
{
    public interface IDataFilterService<T>
    {
        IQueryable<T> Filter(IQueryable<T> data, object filterDto);
    }
}
