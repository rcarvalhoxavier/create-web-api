using System;
using System.Collections.Generic;

namespace com.createwebapi.model.Repositores
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Create(T entity);
        void Create(IList<T> entityList);
        void Update(T entity);
        void Update(IList<T> entityList);
        void Delete(int id);
        void Evict(IList<T> entities);
        void Evict(T entity);
    }
}