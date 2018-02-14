using DataAccessLayer.Abstract;
using DataAccessLayer.Identity;
using DataAccessLayer.Models;
using Entities.Entities;
using Shared.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Library.Shared;
using System.Data.Entity;

namespace Library.DAL.Context
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
                Email = Settings.UserName,
                UserName = Settings.UserName
            };
            var listRoles = new List<string> { Role.Admin.ToString(), Role.User.ToString() };
            SetInitialData(user, listRoles);
            context.SaveChanges();
        }

        public void SetInitialData(ApplicationUser userProfile, List<string> roles)
        {
            var admin = new ApplicationRole { Name = roles[0] };
            _roleManager.Create(admin);
            var user = new ApplicationRole { Name = roles[1] };
            _roleManager.Create(user);
            Create(userProfile);
        }
        public void Create(ApplicationUser userProfile)
        {
            string userPassword = Settings.UserPassword;
            Role userRole = Role.Admin;

            ApplicationUser user = new ApplicationUser { Email = userProfile.Email, UserName = userProfile.Email };
            _userManager.Create(user, userPassword);
            _userManager.AddToRole(user.Id, userRole.ToString());
            ClientProfile clientProfile = new ClientProfile { Id = user.Id };
            _clientManager.Create(clientProfile);
        }
    }
}

