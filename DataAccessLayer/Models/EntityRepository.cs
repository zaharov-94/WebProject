using DataAccessLayer.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Models
{
    public class EntityRepository<T> : IGenericRepository<T> where T : TEntity
    {
        private ApplicationContext _context;
        private DbSet<T> _dbSet;

        public EntityRepository(string connectionString)
        {
           _context = new ApplicationContext(connectionString);
            _dbSet = _context.Set<T>();
        }

        public EntityRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public void Remove(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }
        public virtual void Update(T item)
        {
             _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
