using DataAccessLayer.Abstract;
using Entities.Entities;
using Library.DAL;
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
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            } 
        }

        public T FindById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public IEnumerable<T> GetAll()
        {
            
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            try
            {
                return _dbSet.AsNoTracking().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
                return null;
            }
        }
        public void Remove(int id)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(id));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }
        public virtual void Update(T item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }
    }
}
