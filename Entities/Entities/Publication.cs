using Entities.Entities;
using Library.Entities.Enums;

namespace Library.Web.Entities
{
    public class Publication : TEntity
    {
        public string Name { get; set; }
        public PublicationType Type { get; set; }
    }
}