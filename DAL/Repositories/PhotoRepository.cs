using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using ORM;

namespace DAL.Repositories
{
    public class PhotoRepository : Repository<DALPhoto, Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext context) : base(context) { }

        public IEnumerable<DALPhoto> GetAllByDateCreation(DateTime dateCreation)
        {
            var entities = dbSet.Where(e => e.DateCreation.Date == dateCreation.Date).ToList();

            return entities.Select(e => MapToDALEntity(e));
        }
        public IEnumerable<DALPhoto> GetAllByUser(int userId)
        {
            var entities = dbSet.Where(e => e.UserId == userId).ToList();

            return entities.Select(e => MapToDALEntity(e));
        }

        protected override DALPhoto MapToDALEntity(Photo entity) => Mapper.Map<Photo, DALPhoto>(entity);
        protected override Photo MapToEntity(DALPhoto entity) => Mapper.Map<DALPhoto, Photo>(entity);
    }
}
