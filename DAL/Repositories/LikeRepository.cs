using System.Linq;
using System.Data.Entity;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using ORM;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class LikeRepository : Repository<DALLike, Like>, ILikeRepository
    {
        public LikeRepository(DbContext context) : base(context) { }

        public IEnumerable<DALLike> GetAllLikesForPhoto(int photoId)
        {
            var entities = dbSet.Where(e => e.PhotoId == photoId).ToList();

            return entities.Select(e => MapToDALEntity(e));
        }        
        public IEnumerable<DALLike> GetAllLikesFromUser(int userId)
        {
            var entities = dbSet.Where(e => e.UserId == userId).ToList();

            return entities.Select(e => MapToDALEntity(e));
        }
        public DALLike GetLikeFromUserToPhoto(int userId, int photoId)
        {
            var entity = dbSet.FirstOrDefault(e => e.UserId == userId && e.PhotoId == photoId);

            return MapToDALEntity(entity);
        }

        protected override DALLike MapToDALEntity(Like entity) => Mapper.Map<Like, DALLike>(entity);
        protected override Like MapToEntity(DALLike entity) => Mapper.Map<DALLike, Like>(entity);
    }
}
