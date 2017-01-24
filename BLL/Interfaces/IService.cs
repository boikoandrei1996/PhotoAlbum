using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IService<TEntity>
    {
        void Save(TEntity entity);
        TEntity GetById(int id);
        ICollection<TEntity> GetAll();
        void Delete(int id);
    }
}
