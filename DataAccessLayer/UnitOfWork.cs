using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class UnitOfWork : IDisposable
    {
        private readonly LibraryDbContext _context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }

        public UnitOfWork(string connectionString)
        {
            _context = new LibraryDbContext(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EntityRepository<>);
                if (typeof(T) == typeof(Book))
                {
                    repositoryType = typeof(EntityBookRepository<>);
                }
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            if (typeof(T) == typeof(Book))
            {
                return (EntityBookRepository<T>)repositories[type];
            }
            return (EntityRepository<T>) repositories[type];
        }
    }
}
