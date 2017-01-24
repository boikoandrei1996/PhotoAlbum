using System.Threading;
using System.Threading.Tasks;

using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface IRoleRepositoryAsync : IRepositoryAsync<DALRole>
    {
        Task<DALRole> GetByNameAsync(string name, CancellationToken? token = null);
    }
}
