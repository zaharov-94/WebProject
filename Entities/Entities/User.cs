using Library.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Entities
{
    public class User
    {
        public string Name { get; set; }
        public Roles UserRole { get; set; }
        public string Password { get; set; }
    }
}