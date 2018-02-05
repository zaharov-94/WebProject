using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ApplicationUser : IUser
    {
        public ApplicationUser(string name)
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;
        }
        [Key]
        public string Id { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
