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
        Task<OperationDetails> Create(UserTable userDto);
        Task<ClaimsIdentity> Authenticate(UserTable userDto);
        Task SetInitialData(UserTable adminDto, List<string> roles);
        Task SetInitialDataAsync();
    } 
}
