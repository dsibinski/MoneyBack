using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MoneyBack.Orm
{
    public interface IRepository<T> where T : class, new()
    {
        IList<T> GetAll();
        IList<T> GetAllWithChildren();
        T Get(int id);
        T GetWithChildren(int id);
        IList<T> Get(Expression<Func<T, bool>> filter);

        int Insert(T entity);
        void InsertWithChildren(T entity);
        void Update(T entity);
        void UpdateWithChildren(T entity);
        int Delete(T entity);

    }
}