using Entities.Entities;
using System;

namespace DataAccessLayer.Abstract
{
    public interface IClientManager:IDisposable
    {
        void Create(ClientProfile item);
    }
}
