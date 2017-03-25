using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace MoneyBack.Orm
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IList<T>> GetAll();
        Task<T> Get(int id);
        Task<IList<T>> Get(Expression<Func<T, bool>> filter);

        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);

    }
}