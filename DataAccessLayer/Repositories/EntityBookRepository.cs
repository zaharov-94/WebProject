using Library.DAL;
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

        public EntityBookRepository(ApplicationContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public override void Add(T item)
        {
            try
            {
                Book entity = new Book { Id = item.Id, Name = item.Name, Author = item.Author,
                    YearOfPublishing = item.YearOfPublishing, PublicationHouses = new List<PublicationHouse>()};
                _context.Books.Add(entity);
                _context.SaveChanges();
                List<PublicationHouse> list = _context.PublicationHouses.ToList();
                foreach (var iterator in item.PublicationHouses)
                {
                    entity.PublicationHouses.Add(list.Find(x => x.Id == iterator.Id));
                }
                
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
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
                LogRegistrator.Write(ex);
            }
        }
    }
}
