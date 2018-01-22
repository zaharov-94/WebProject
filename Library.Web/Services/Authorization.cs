using Library.Web.Entities;
using Library.Web.Enums;
using Library.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web
{
    public class Authorization
    {
        UserRepository _userRepository;
        public Authorization()
        {
            _userRepository = new UserRepository();
        }
        public bool IsAuth(User user)
        {
           if(user.Password == _userRepository.GetPassword(user.Name))
            {
                return true;
            }
            return false;
        }
        public Roles GetRole(User user)
        {
            return _userRepository.GetRole(user.Name);
        }
    }
}