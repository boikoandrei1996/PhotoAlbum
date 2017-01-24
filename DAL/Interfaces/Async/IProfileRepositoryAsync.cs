using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface IProfileRepositoryAsync : IRepositoryAsync<DALProfile>
    {
        Task<IEnumerable<DALProfile>> GetAllByNameAsync(string firstOrLastName, CancellationToken? token = null);
    }
}
