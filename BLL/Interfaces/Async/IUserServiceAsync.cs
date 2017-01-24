using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces.Async
{
    public interface IUserServiceAsync : IServiceAsync<BLLUser>
    {
        Task<bool> CheckPasswordAsync(string login, string password, CancellationToken? token = null);
        Task<BLLUser> GetByLoginAsync(string login, CancellationToken? token = null);
        Task<BLLUser> GetPhotoUserAsync(BLLPhoto photo, CancellationToken? token = null);
        Task<ICollection<BLLUser>> GetUsersWithRoleAsync(BLLRole role, CancellationToken? token = null);
    }
}
