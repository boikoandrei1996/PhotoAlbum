using System.Collections.Generic;

using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ILikeRepository : IRepository<DALLike>
    {
        IEnumerable<DALLike> GetAllLikesFromUser(int userId);
        IEnumerable<DALLike> GetAllLikesForPhoto(int photoId);
        DALLike GetLikeFromUserToPhoto(int userId, int photoId);
    }
}
