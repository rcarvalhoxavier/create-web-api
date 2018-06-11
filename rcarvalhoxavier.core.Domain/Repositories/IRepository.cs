using System;
using System.Linq;
using rcarvalhoxavier.core.Domain.Entities;

namespace rcarvalhoxavier.core.Domain.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
