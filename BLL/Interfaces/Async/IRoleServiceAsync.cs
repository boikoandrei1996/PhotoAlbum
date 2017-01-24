using System.Threading;
using System.Threading.Tasks;

using BLL.Entities;

namespace BLL.Interfaces.Async
{
    public interface IRoleServiceAsync : IServiceAsync<BLLRole>
    {
        Task<BLLRole> GetByNameAsync(string name, CancellationToken? token = null);
        Task<BLLRole> GetUserRoleAsync(BLLUser user, CancellationToken? token = null);
    }
}
