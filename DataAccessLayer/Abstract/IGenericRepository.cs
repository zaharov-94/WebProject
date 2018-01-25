using System;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(int id);
        void Update(TEntity item);
    }
}
