using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MoneyBack.Orm
{
    public class SqliteRepository<T> : SQLiteConnection, IRepository<T> where T : class, IEntity, new()
    {
        public SqliteRepository(string dbFilePath)
        : base(dbFilePath)
        {
            CreateTable<T>();
        }

        public IList<T> GetAll()
        {
            return Table<T>().ToList();
        }

        public IList<T> GetAllWithChildren()
        {
            return this.GetAllWithChildren<T>();
        }


        public T Get(int id)
        {
            return Get<T>(id);
        }

        public T GetWithChildren(int id)
        {
            return this.GetWithChildren<T>(id);
        }

        public IList<T> Get(Expression<Func<T, bool>> filter)
        {
            return Table<T>().Where(filter).ToList();
        }


        int IRepository<T>.Insert(T entity)
        {
            return Insert(entity);
        }

        void IRepository<T>.InsertWithChildren(T entity)
        {
            this.InsertWithChildren(entity);
        }

        void IRepository<T>.Update(T entity)
        {
            this.Update(entity);
        }

        void IRepository<T>.UpdateWithChildren(T entity)
        {
            this.UpdateWithChildren(entity);
        }

        int IRepository<T>.Delete(T entity)
        {
            return this.Delete<T>(entity.Id);
        }

        
    }
}