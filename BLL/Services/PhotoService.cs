using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces;

using System.Text.RegularExpressions;

namespace BLL.Services
{
    public class PhotoService : Service<BLLPhoto, DALPhoto>, IPhotoService
    {
        private readonly IPhotoRepository repositoryPhoto;
        public PhotoService(IUnitOfWork _context) : base(_context, _context.Photos)
        {
            repositoryPhoto = context.Photos;
        }

        public ICollection<BLLPhoto> GetAllByDateCreation(DateTime dateCreation) =>
            repositoryPhoto.GetAllByDateCreation(dateCreation).Select(e => MapToBLLEntity(e)).ToList();

        public ICollection<BLLPhoto> GetUserPhotos(int userId)
        {
            var photos = repositoryPhoto.GetAllByUser(userId);
            
            return photos.Select(e => MapToBLLEntity(e)).ToList();
        }

        public ICollection<BLLPhoto> GetPhotosByDescription(string pattern, TimeSpan? timeOutPerMatch = null)
        {
            var photos = new List<BLLPhoto>();

            if (pattern == null || pattern == string.Empty)
                return photos;

            Regex regex;
            if (timeOutPerMatch == null)
                regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            else
                regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase, timeOutPerMatch.Value);

            foreach (var photo in repositoryPhoto.GetAll())
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
