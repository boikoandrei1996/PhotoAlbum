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
using System.Text.RegularExpressions;

namespace BLL.Services.Async
{
    public class PhotoServiceAsync : ServiceAsync<BLLPhoto, DALPhoto>, IPhotoServiceAsync
    {
        private readonly IPhotoRepositoryAsync repositoryPhoto;
        public PhotoServiceAsync(IUnitOfWorkAsync _context) : base(_context, _context.Photos)
        {
            repositoryPhoto = context.Photos;
        }

        public async Task<ICollection<BLLPhoto>> GetAllByDateCreationAsync(DateTime dateCreation, CancellationToken? token = null)
        {
            var entities = await repositoryPhoto.GetAllByDateCreationAsync(dateCreation, token);
            return entities.Select(e => MapToBLLEntity(e)).ToList();
        }

        public async Task<ICollection<BLLPhoto>> GetUserPhotosAsync(int userId, CancellationToken? token = null)
        {
            var photos = await repositoryPhoto.GetAllByUserAsync(userId, token);

            return photos.Select(e => MapToBLLEntity(e)).ToList();
        }

        public async Task<ICollection<BLLPhoto>> GetPhotosByDescriptionAsync(string pattern, TimeSpan? timeOutPerMatch = null, CancellationToken? token = null)
        {
            var photos = new List<BLLPhoto>();

            if (pattern == null || pattern == string.Empty)
                return photos;

            Regex regex;
            if (timeOutPerMatch == null)
                regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            else
                regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, timeOutPerMatch.Value);

            var allPhotos = await repositoryPhoto.GetAllAsync(token);
            foreach (var photo in allPhotos)
                if (photo.Description != null && regex.IsMatch(photo.Description))
                    photos.Add(MapToBLLEntity(photo));

            return photos;
        }

        protected override BLLPhoto MapToBLLEntity(DALPhoto entity) => 
            Mapper.Map<DALPhoto, BLLPhoto>(entity);
        protected override DALPhoto MapToDALEntity(BLLPhoto entity) => 
            Mapper.Map<BLLPhoto, DALPhoto>(entity);
    }
}
