using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Entities
{
    public class Publication : TEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}