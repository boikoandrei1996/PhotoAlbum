using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ILikeService : IService<BLLLike>
    {
        ICollection<BLLLike> GetLikesFromUser(int userId);
        ICollection<BLLLike> GetLikesForPhoto(int photoId);
        BLLLike GetLikeFromUserToPhoto(int userId, int photoId);
    }
}
