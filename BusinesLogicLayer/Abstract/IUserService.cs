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
        Task<OperationDetails> Create(UserViewModel userTable);
        Task<ClaimsIdentity> Authenticate(UserViewModel userTable);
        Task SetInitialData(UserViewModel adminTable, List<string> roles);
    } 
}
