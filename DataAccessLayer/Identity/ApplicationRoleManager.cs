using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Entities.Entities;

namespace DataAccessLayer.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
