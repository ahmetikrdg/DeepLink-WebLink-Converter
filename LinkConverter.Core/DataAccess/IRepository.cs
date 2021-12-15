using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkConverter.Core.DataAccess
{
    public interface IRepository<T>
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        Task Create(T Entity);

    }
}
    