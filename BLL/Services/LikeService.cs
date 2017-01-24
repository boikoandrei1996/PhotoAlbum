using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class LikeService : Service<BLLLike, DALLike>, ILikeService
    {
        private readonly ILikeRepository repositoryLike;
        public LikeService(IUnitOfWork _context) : base(_context, _context.Likes)
        {
            repositoryLike = context.Likes;
        }

        public ICollection<BLLLike> GetLikesFromUser(int userId)
        {
            var likes = repositoryLike.GetAllLikesFromUser(userId);

            return likes.Select(e => MapToBLLEntity(e)).ToList();
        }
        public ICollection<BLLLike> GetLikesForPhoto(int photoId)
        {
            var likes = repositoryLike.GetAllLikesForPhoto(photoId);

            return likes.Select(e => MapToBLLEntity(e)).ToList();
        }
        public BLLLike GetLikeFromUserToPhoto(int userId, int photoId)
        {
            var like = repositoryLike.GetLikeFromUserToPhoto(userId, photoId);

            return MapToBLLEntity(like);
        }
        
        protected override BLLLike MapToBLLEntity(DALLike entity) => 
            Mapper.Map<DALLike, BLLLike>(entity);
        protected override DALLike MapToDALEntity(BLLLike entity) => 
            Mapper.Map<BLLLike, DALLike>(entity);
    }
}
