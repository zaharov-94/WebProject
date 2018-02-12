using DataAccessLayer.Abstract;
using DataAccessLayer.Identity;
using DataAccessLayer.Models;
using Entities.Entities;
using Library.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class DataInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        IClientManager _clientManager;

        protected override async void Seed(ApplicationContext context)
        {
            base.Seed(context);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            _clientManager = new ClientManager(context);

            var user = new ApplicationUser
            {
                Email = "ser",
                UserName = "ser"
            };
            var listRoles = new List<string> { Role.Admin.ToString(), Role.User.ToString() };
            await SetInitialData(user, listRoles);
            await context.SaveAsync();
        }

        public async Task SetInitialData(ApplicationUser userProfile, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = _roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _roleManager.CreateAsync(role);
                }
            }

            await Create(userProfile);
        }
        public async Task Create(ApplicationUser userProfile)
        {
            string userPassword = "123456";
            Role userRole = Role.Admin;

            ApplicationUser user = new ApplicationUser { Email = userProfile.Email, UserName = userProfile.Email };
            await _userManager.CreateAsync(user, userPassword);
            await _userManager.AddToRoleAsync(user.Id, userRole.ToString());
            ClientProfile clientProfile = new ClientProfile { Id = user.Id };
            _clientManager.Create(clientProfile);
        }
    }
}

