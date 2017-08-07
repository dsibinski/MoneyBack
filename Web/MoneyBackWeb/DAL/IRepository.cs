using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyBackWeb.DAL
{
    public interface IRepository<T> : IDisposable
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Find(int id);

        IQueryable<T> Query();

    }
}
