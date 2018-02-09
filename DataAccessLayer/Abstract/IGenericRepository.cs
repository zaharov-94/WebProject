using System;
using System.Collections.Generic;
using Entities.Entities;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : TEntity
    {
        void Add(T item);
        T FindById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Remove(int id);
        void Update(T item);
    }
}
