using Entities.Entities;
using Library.DAL.Repositories;
using Library.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<PublicationHouse> PublicationHouses { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public ApplicationContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<ApplicationContext>(new CreateDatabaseIfNotExists<ApplicationContext>());
            Database.SetInitializer<ApplicationContext>(new DataInitializer());
        }
        //public ApplicationContext() : base("PublicationsContext")
        //{
        //    Database.SetInitializer<ApplicationContext>(new CreateDatabaseIfNotExists<ApplicationContext>());
        //    Database.SetInitializer<ApplicationContext>(new DataInitializer());
        //}
        public static ApplicationContext Create()
        {
            return new ApplicationContext("PublicationsContext");
        }

        internal Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
