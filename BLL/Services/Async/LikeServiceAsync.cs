using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces.Async;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services.Async
{
    public class LikeServiceAsync : ServiceAsync<BLLLike, DALLike>, ILikeServiceAsync
    {
        private readonly ILikeRepositoryAsync repositoryLike;
        public LikeServiceAsync(IUnitOfWorkAsync _context) : base(_context, _context.Likes)
        {
            repositoryLike = context.Likes;
        }

        public async Task<ICollection<BLLLike>> GetLikesFromUserAsync(int userId, CancellationToken? token = null)
        {
            var likes = await repositoryLike.GetAllLikesFromUserAsync(userId, token);

            return likes.Select(e => MapToBLLEntity(e)).ToList();
        }
        public async Task<ICollection<BLLLike>> GetLikesForPhotoAsync(int photoId, CancellationToken? token = null)
        {
            var likes = await repositoryLike.GetAllLikesForPhotoAsync(photoId, token);

            return likes.Select(e => MapToBLLEntity(e)).ToList();
        }
        public async Task<BLLLike> GetLikeFromUserToPhotoAsync(int userId, int photoId, CancellationToken? token = null)
        {
            var like = await repositoryLike.GetLikeFromUserToPhotoAsync(userId, photoId, token);

            return MapToBLLEntity(like);
        }

        protected override BLLLike MapToBLLEntity(DALLike entity) => 
            Mapper.Map<DALLike, BLLLike>(entity);
        protected override DALLike MapToDALEntity(BLLLike entity) => 
            Mapper.Map<BLLLike, DALLike>(entity);
    }
}
