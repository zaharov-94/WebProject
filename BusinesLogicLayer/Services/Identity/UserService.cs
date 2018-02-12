using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using BusinesLogicLayer.Abstract;
using BusinesLogicLayer.Infrastructure;
using Entities.Tables;
using Entities.Entities;
using DataAccessLayer;

namespace BusinesLogicLayer.Services.Identity
{
    public class UserService : IUserService
    {
        UnitOfWork Database { get; set; }

        public UserService(UnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserViewModel userTable)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userTable.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userTable.Email, UserName = userTable.Email };
                await Database.UserManager.CreateAsync(user, userTable.Password);

                await Database.UserManager.AddToRoleAsync(user.Id, userTable.Role.ToString());

                ClientProfile clientProfile = new ClientProfile { Id = user.Id };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails {Succedeed= true, Message = "Registration success", Property=  ""};
            }
            else
            {
                return new OperationDetails { Succedeed= false, Message = "User alredy exist", Property = "Email" };
            }
        }

        

        public async Task<ClaimsIdentity> Authenticate(UserViewModel userTable)
        {
            ClaimsIdentity claim = null;
            
            ApplicationUser user = await Database.UserManager.FindAsync(userTable.Email, userTable.Password);
            
            if(user!=null)
                claim= await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        
        public async Task SetInitialData(UserViewModel adminTable, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName};
                    await Database.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminTable);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }

    
}
