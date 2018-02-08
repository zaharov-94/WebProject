using Entities.Entities;
using Library.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataAccessLayer.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public ApplicationContext() : base("PublicationsContext") { }
        public ApplicationContext(string connectionString) : base(connectionString)
        { }
        public static ApplicationContext Create()
        {
            return new ApplicationContext("PublicationsContext");
        }
    }
}
