using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Models
{
    class EntityBookRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private LibraryDbContext _context;
        private DbSet<TEntity> _dbSet;

        public EntityBookRepository(string connectionString)
        {
            _context = new LibraryDbContext(connectionString);
            _dbSet = _context.Set<TEntity>();
        }

        public EntityBookRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public void Remove(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            //foreach (var item in entity.SelectedPublicationHouses)
            //{
            //    if (_context.PublicationHouses.Find(item).Books.Where(x => x.Id == id).Count() == 0)
            //    {
            //        _context.Books.Find(id).PublicationHouses.Add(_context.PublicationHouses.Find(item));
            //    }
            //}
            //List<PublicationHouse> listRemove = new List<PublicationHouse>();
            //foreach (var item in _context.Books.Find(id).PublicationHouses)
            //{
            //    if (!entity.SelectedPublicationHouses.Contains(item.Id))
            //    {
            //        listRemove.Add(item);
            //    }
            //}
            //foreach (var item in listRemove)
            //{
            //    _context.Books.Find(id).PublicationHouses.Remove(item);
            //}
            //Book book = new Book
            //{
            //    Name = entity.Book.Name,
            //    Author = entity.Book.Author,
            //    YearOfPublishing = entity.Book.YearOfPublishing
            //};
            ////_bookService.Edit(book);
            //_context.Books.Find(id).Name = book.Name;
            //_context.Books.Find(id).Author = book.Author;
            //_context.Books.Find(id).YearOfPublishing = book.YearOfPublishing;
            //_context.Entry(book).State = System.Data.Entity.EntityState.Detached;
            //_context.SaveChanges();
            ////_context.Entry(item).State = EntityState.Modified;
            ////_context.SaveChanges();
        }
    }
}
