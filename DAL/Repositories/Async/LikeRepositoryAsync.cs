using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using ORM;
using System.Collections.Generic;

namespace DAL.Repositories.Async
{
    public class LikeRepositoryAsync : RepositoryAsync<DALLike, Like>, ILikeRepositoryAsync
    {
        public LikeRepositoryAsync(DbContext context) : base(context) { }
        
        public async Task<IEnumerable<DALLike>> GetAllLikesForPhotoAsync(int photoId, CancellationToken? token = null)
        {
            List<Like> entities;
            if (token == null)
                entities = await dbSet.Where(e => e.PhotoId == photoId).ToListAsync();
            else
                entities = await dbSet.Where(e => e.PhotoId == photoId).ToListAsync(token.Value);

            return entities.Select(e => MapToDALEntity(e));
        }
        public async Task<IEnumerable<DALLike>> GetAllLikesFromUserAsync(int userId, CancellationToken? token = null)
        {
            List<Like> entities;
            if (token == null)
                entities = await dbSet.Where(e => e.UserId == userId).ToListAsync();
            else
                entities = await dbSet.Where(e => e.UserId == userId).ToListAsync(token.Value);

            return entities.Select(e => MapToDALEntity(e));
        }
        public async Task<DALLike> GetLikeFromUserToPhotoAsync(int userId, int photoId, CancellationToken? token = null)
        {
            Like like;

            if (token == null)
                like = await dbSet.FirstOrDefaultAsync(e => e.UserId == userId && e.PhotoId == photoId);
            else
                like = await dbSet.FirstOrDefaultAsync(e => e.UserId == userId && e.PhotoId == photoId, token.Value);

            return MapToDALEntity(like);
        }

        protected override DALLike MapToDALEntity(Like entity) => Mapper.Map<Like, DALLike>(entity);
        protected override Like MapToEntity(DALLike entity) => Mapper.Map<DALLike, Like>(entity);
    }
}
