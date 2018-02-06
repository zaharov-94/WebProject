﻿using BusinesLogicLayer.Abstract;
using DataAccessLayer.Models;

namespace BusinesLogicLayer.Services.Identity
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
