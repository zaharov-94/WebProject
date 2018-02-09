using BusinesLogicLayer.Infrastructure;
using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BusinesLogicLayer.Abstract
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserTable userTable);
        Task<ClaimsIdentity> Authenticate(UserTable userTable);
        Task SetInitialData(UserTable adminTable, List<string> roles);
    } 
}
