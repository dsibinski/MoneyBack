using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MoneyBack.Helpers;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync.Extensions;

namespace MoneyBack.Orm
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private SQLiteAsyncConnection dbConnection = null;

        public Repository(SQLiteAsyncConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            dbConnection.CreateTableAsync<T>();
        }



        public async Task<IList<T>> GetAll()
        {
            return await dbConnection.GetAllWithChildrenAsync<T>();
        }


        public async Task<T> Get(int id)
        {
            return await dbConnection.GetWithChildrenAsync<T>(id);
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> filter)
        {
            return await dbConnection.GetAllWithChildrenAsync(filter);
        }

        public async Task<int> Insert(T entity)
        {
            return await dbConnection.InsertAsync(entity);
        }

        public async Task InsertWithChildren(T entity)
        {
            await dbConnection.InsertWithChildrenAsync(entity);
        }

        public async Task Update(T entity)
        {
            await dbConnection.UpdateWithChildrenAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await dbConnection.DeleteAsync(entity);
        }

        
    }
}