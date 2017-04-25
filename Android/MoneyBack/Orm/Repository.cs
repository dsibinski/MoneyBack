using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MoneyBack.Helpers;
using SQLite;

namespace MoneyBack.Orm
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private SQLiteAsyncConnection db = null;

        public Repository() : this(new SQLiteAsyncConnection(Constants.DbFilePath)) { }


        public Repository(SQLiteAsyncConnection db)
        {
            this.db = db;
            db.CreateTableAsync<T>();
        }

        public async Task<IList<T>> GetAll()
        {
            return await db.Table<T>().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await db.FindAsync<T>(id);
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> filter)
        {
            var table = db.Table<T>();

            if (filter != null)
                table = table.Where(filter);

            return await table.ToListAsync();

        }

        public async Task<int> Insert(T entity)
        {
            return await db.InsertAsync(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await db.UpdateAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await db.DeleteAsync(entity);
        }

        
    }
}