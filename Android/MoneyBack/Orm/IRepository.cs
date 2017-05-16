using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyBack.Orm
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task<IList<T>> Get(Expression<Func<T, bool>> filter);

        Task<int> Insert(T entity);
        Task InsertWithChildren(T entity);
        Task Update(T entity);
        Task<int> Delete(T entity);

    }
}