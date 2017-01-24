using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface IUserRepositoryAsync : IRepositoryAsync<DALUser>
    {
        Task<DALUser> GetByLoginAsync(string login, CancellationToken? token = null);
    }
}
