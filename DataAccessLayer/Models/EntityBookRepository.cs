using Library.Web.Entities;
using Library.Web.View_models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Models
{
    public class EntityBookRepository<TEntity> : EntityRepository<TEntity> where TEntity : class
    {
        private LibraryDbContext _context;
        private DbSet<TEntity> _dbSet;

        public EntityBookRepository(string connectionString) : base (connectionString)
        {
            _context = new LibraryDbContext(connectionString);
            _dbSet = _context.Set<TEntity>();
        }

        public EntityBookRepository(LibraryDbContext context) : base (context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public override void Update(object bookViewModel)
        {
            BookViewModel entity = (BookViewModel)bookViewModel;
            //Заменить selected с int на объект
            Book book = _context.Books.Find(entity.Book.Id);
            foreach (var property in entity.Book.GetType().GetProperties())
            {
                book.GetType().GetProperty(property.Name).SetValue(book, property.GetValue(entity.Book));
            }

            book.PublicationHouses.Clear();
            List<PublicationHouse> list = _context.PublicationHouses.ToList();

            foreach (var item in entity.PublicationHouses)
            {
                book.PublicationHouses.Add(list.Find( x => x.Id == item));
            }
            _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
