using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoneyBackWeb.DAL
{
    public class DbContextRepository<TContext, T> : IRepository<T>
        where T : class
        where TContext : DbContext, new()
    {

        private TContext _context;
        private DbSet<T> _dbSet;

        public DbContextRepository(Func<TContext, DbSet<T>> entitiesSelector)
        {
            _context = new TContext();
            _dbSet = entitiesSelector(_context);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context?.Dispose();
                _context = null;
            }

        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
