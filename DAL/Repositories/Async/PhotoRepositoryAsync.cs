using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using ORM;

namespace DAL.Repositories.Async
{
    public class PhotoRepositoryAsync : RepositoryAsync<DALPhoto, Photo>, IPhotoRepositoryAsync
    {
        public PhotoRepositoryAsync(DbContext context) : base(context) { }
        
        public async Task<IEnumerable<DALPhoto>> GetAllByDateCreationAsync(DateTime dateCreation, CancellationToken? token = null)
        {
            List<Photo> entities;
            if (token == null)
                entities = await dbSet.Where(e => e.DateCreation.Date == dateCreation.Date).ToListAsync();
            else
                entities = await dbSet.Where(e => e.DateCreation.Date == dateCreation.Date).ToListAsync(token.Value);

            return entities.Select(e => MapToDALEntity(e));
        }

        public async Task<IEnumerable<DALPhoto>> GetAllByUserAsync(int userId, CancellationToken? token = null)
        {
            List<Photo> entities;

            if (token == null)
                entities = await dbSet.Where(e => e.UserId == userId).ToListAsync();
            else
                entities = await dbSet.Where(e => e.UserId == userId).ToListAsync(token.Value);

            return entities.Select(e => MapToDALEntity(e));
        }

        protected override DALPhoto MapToDALEntity(Photo entity) => Mapper.Map<Photo, DALPhoto>(entity);
        protected override Photo MapToEntity(DALPhoto entity) => Mapper.Map<DALPhoto, Photo>(entity);
    }
}
