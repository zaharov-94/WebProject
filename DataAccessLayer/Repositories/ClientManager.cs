using DataAccessLayer.Abstract;
using Entities.Entities;
using Library.DAL;
using Library.DAL.Context;
using System;

namespace DataAccessLayer.Models
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            try
            {
                Database.ClientProfiles.Add(item);
                Database.SaveChanges();
            }
            catch (Exception ex)
            {
                LogRegistrator.Write(ex);
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
