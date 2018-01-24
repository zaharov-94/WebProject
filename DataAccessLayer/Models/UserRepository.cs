using Library.Web.Entities;
using Library.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class UserRepository
    {
        private List<User> _users;
        public UserRepository()
        {
            _users = new List<User>();
            _users.Add(new User { Name = "Serhii", UserRole = Enums.Roles.User, Password = "12345" });
            _users.Add(new User { Name = "Admin", UserRole = Enums.Roles.Administrator, Password = "12345" });
        }

        public string GetPassword(string userName)
        {
            return _users.FirstOrDefault(x => x.Name == userName).Password;
        }
        public Roles GetRole(string userName)
        {
            return _users.FirstOrDefault(x => x.Name == userName).UserRole;
        }
    }
}