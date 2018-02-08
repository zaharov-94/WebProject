using DataAccessLayer.Abstract;
using DataAccessLayer.Identity;
using DataAccessLayer.Models;
using Entities.Entities;
using Entities.Enums;
using Entities.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var userTable = new UserTable
            {
                Email = "ser",
                UserName = "ser",
                Password = "123456",
                Role = Role.Admin,
            };
            var listRoles = new List<string> { Role.Admin.ToString(), Role.User.ToString() };
            SetInitialData(userTable, listRoles);
            context.SaveChanges();
        }

        public void SetInitialData(UserTable adminTable, List<string> roles)
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

            Create(adminTable);
        }
        public void Create(UserTable userTable)
        {
            ApplicationUser user = _userManager.FindByEmail(userTable.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userTable.Email, UserName = userTable.Email };
                _userManager.Create(user, userTable.Password);

                _userManager.AddToRole(user.Id, userTable.Role.ToString());

                ClientProfile clientProfile = new ClientProfile { Id = user.Id };
                _clientManager.Create(clientProfile);
            }
        }
    }
}

