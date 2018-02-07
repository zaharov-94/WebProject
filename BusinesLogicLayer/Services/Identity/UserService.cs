using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using DataAccessLayer.Abstract;
using BusinesLogicLayer.Abstract;
using BusinesLogicLayer.Infrastructure;
using Entities.Tables;
using Entities.Entities;

namespace BusinesLogicLayer.Services.Identity
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserTable userTable)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userTable.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userTable.Email, UserName = userTable.Email };
                await Database.UserManager.CreateAsync(user, userTable.Password);
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userTable.Role.ToString());
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userTable.Address, Name = userTable.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");

            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserTable userTable)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userTable.Email, userTable.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if(user!=null)
                claim= await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserTable adminTable, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
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
