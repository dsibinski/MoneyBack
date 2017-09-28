using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Realms;

namespace MoneyBack.Orm
{
    public class RealmRepository<T> : IRepository<T> where T : RealmObject, IEntity, new()
    {
        private Realm realm => Realm.GetInstance();

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAllWithChildren()
        {
            throw new NotImplementedException();
        }

        public T GetWithChildren(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            realm.Write(() =>
            {
                realm.Add(entity);
            });
        }

        public void InsertWithChildren(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithChildren(T entity)
        {
            throw new NotImplementedException();
        }
    }
}