using Entities.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CustomUserStore : IUserStore<ApplicationUser>
    {
        private LibraryDbContext _context;
        
        public CustomUserStore(string connectionString)
        {
            _context = new LibraryDbContext(connectionString);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => _context.Users.Add(user));
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => _context.Users.FirstOrDefault(u => u.UserName == userName));
        }
    }
}
