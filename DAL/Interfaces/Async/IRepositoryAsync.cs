using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface IRepositoryAsync<TEntity> where TEntity : DALEntity
    {
        Task CreateAsync(TEntity entity, CancellationToken? token = null);        
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken? token = null);
        Task<TEntity> GetByIdAsync(int id, CancellationToken? token = null);
        Task UpdateAsync(TEntity entity, CancellationToken? token = null);
        Task DeleteAsync(int id, CancellationToken? token = null);
    }
}
