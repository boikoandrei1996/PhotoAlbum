using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : DALEntity
    {
        void Create(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
