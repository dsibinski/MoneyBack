using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MoneyBack.Helpers;
using SQLite;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Extensions;

namespace MoneyBack.Orm
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private SQLiteConnection dbConnection = null;

        public Repository(SQLiteConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            dbConnection.CreateTable<T>();
        }


        public IList<T> GetAll()
        {
            return dbConnection.Table<T>().ToList();
        }

        public IList<T> GetAllWithChildren()
        {
            return dbConnection.GetAllWithChildren<T>();
        }


        public T Get(int id)
        {
            return dbConnection.Get<T>(id);
        }

        public T GetWithChildren(int id)
        {
            return dbConnection.GetWithChildren<T>(id);
        }

        public IList<T> Get(Expression<Func<T, bool>> filter)
        {
            return dbConnection.Table<T>().Where(filter).ToList();
        }

        public int Insert(T entity)
        {
            return dbConnection.Insert(entity);
        }

        public void InsertWithChildren(T entity)
        {
            dbConnection.InsertWithChildren(entity);
        }

        public void Update(T entity)
        {
            dbConnection.Update(entity);
        }

        public void UpdateWithChildren(T entity)
        {
            dbConnection.UpdateWithChildren(entity);
        }
        public int Delete(T entity)
        {
            return dbConnection.Delete<T>(entity.Id);
        }

        
    }
}