﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Web.Entities
{
    public class ApplicationRole
    {
        [Key]
        public int Id { get; set; }
        public Library.Web.Enums.Roles Role { get; set; }
    }
}
