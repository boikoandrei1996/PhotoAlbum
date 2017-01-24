using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using DAL.Entities;

namespace DAL.Interfaces.Async
{
    public interface ILikeRepositoryAsync : IRepositoryAsync<DALLike>
    {
        Task<IEnumerable<DALLike>> GetAllLikesFromUserAsync(int userId, CancellationToken? token = null);        
        Task<IEnumerable<DALLike>> GetAllLikesForPhotoAsync(int photoId, CancellationToken? token = null);
        Task<DALLike> GetLikeFromUserToPhotoAsync(int userId, int photoId, CancellationToken? token = null);
    }
}
