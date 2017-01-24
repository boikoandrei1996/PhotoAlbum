using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces.Async
{
    public interface IProfileServiceAsync : IServiceAsync<BLLProfile>
    {
        Task<ICollection<BLLProfile>> GetAllByNameAsync(string firstOrLastName, CancellationToken? token = null);
    }
}
