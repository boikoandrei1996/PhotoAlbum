using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BLL.Entities;

namespace BLL.Interfaces.Async
{
    public interface ILikeServiceAsync : IServiceAsync<BLLLike>
    {
        Task<ICollection<BLLLike>> GetLikesFromUserAsync(int userId, CancellationToken? token = null);
        Task<ICollection<BLLLike>> GetLikesForPhotoAsync(int photoId, CancellationToken? token = null);
        Task<BLLLike> GetLikeFromUserToPhotoAsync(int userId, int photoId, CancellationToken? token = null);
    }
}
