using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces.Async
{
    public interface IServiceAsync<TEntity>
    {
        Task SaveAsync(TEntity entity, CancellationToken? token = null);
        Task<TEntity> GetByIdAsync(int id, CancellationToken? token = null);
        Task<ICollection<TEntity>> GetAllAsync(CancellationToken? token = null);
        Task DeleteAsync(int id, CancellationToken? token = null);
    }
}
