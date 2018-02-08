using Entities.Entities;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Models
{
    public class EntityBookRepository<T> : EntityRepository<T> where T : Book
    {
        private ApplicationContext _context;
        private DbSet<T> _dbSet;

        public EntityBookRepository(string connectionString) : base (connectionString)
        {
            _context = new ApplicationContext(connectionString);
            _dbSet = _context.Set<T>();
        }

        public EntityBookRepository(ApplicationContext context) : base (context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public override void Update(T item)
        {
            try
            {
                Book entity = (Book)item;
                Book book = _context.Books.Find(entity.Id);

                book.PublicationHouses.Clear();
                List<PublicationHouse> list = _context.PublicationHouses.ToList();
                foreach (var iterator in entity.PublicationHouses)
                {
                    book.PublicationHouses.Add(list.Find(x => x.Id == iterator.Id));
                }
                _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException + " " + ex.Message);
            }
        }
    }
}
