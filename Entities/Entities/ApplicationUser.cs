using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Web.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        public ApplicationUser()
        {

        }
    }
}
