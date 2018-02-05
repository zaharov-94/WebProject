using Entities.Entities;
using Library.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataAccessLayer.Models
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }

        public LibraryDbContext() : base("PublicationsContext") { }
        public LibraryDbContext(string connectionString) : base(connectionString)
        { }
        public static LibraryDbContext Create()
        {
            return new LibraryDbContext();
        }
    }
}
