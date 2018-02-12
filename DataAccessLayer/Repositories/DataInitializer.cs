using DataAccessLayer.Abstract;
using DataAccessLayer.Identity;
using DataAccessLayer.Models;
using Entities.Entities;
using Library.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Library.DAL.Repositories
{
    public class DataInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        IClientManager _clientManager;

        protected override void Seed(ApplicationContext context)
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
            SetInitialData(user, listRoles);
            context.SaveChanges();
        }

        public void SetInitialData(ApplicationUser userProfile, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = _roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    _roleManager.CreateAsync(role);
                }
            }

            Create(userProfile);
        }
        public void Create(ApplicationUser userProfile)
        {
            string userPassword = "123456";
            Role userRole = Role.Admin;

            ApplicationUser user = new ApplicationUser { Email = userProfile.Email, UserName = userProfile.Email };
            _userManager.Create(user, userPassword);
            _userManager.AddToRole(user.Id, userRole.ToString());
            ClientProfile clientProfile = new ClientProfile { Id = user.Id };
            _clientManager.Create(clientProfile);
        }
    }
}

