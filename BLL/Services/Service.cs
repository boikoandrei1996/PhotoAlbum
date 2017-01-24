using System;
using System.Collections.Generic;
using System.Linq;

using DAL.Entities;
using DAL.Interfaces;
using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public abstract class Service<TBLLEntity, TDALEntity> : IService<TBLLEntity>
        where TBLLEntity : BLLEntity
        where TDALEntity : DALEntity
    {
        protected readonly IUnitOfWork context;
        private readonly IRepository<TDALEntity> repositoryBase;
        public Service(IUnitOfWork _context, IRepository<TDALEntity> _repository)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            if (_repository == null)
                throw new ArgumentNullException(nameof(_repository));

            context = _context;
            repositoryBase = _repository;
        }

        public virtual void Save(TBLLEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Id));

            if (entity.Id == 0)
                repositoryBase.Create(MapToDALEntity(entity));
            else
                repositoryBase.Update(MapToDALEntity(entity));                

            context.Commit();
        }
        public virtual void Delete(int id)
        {
            repositoryBase.Delete(id);
            context.Commit();
        }
        public virtual TBLLEntity GetById(int id) => MapToBLLEntity(repositoryBase.GetById(id));
        public virtual ICollection<TBLLEntity> GetAll() 
            => repositoryBase.GetAll().Select(entity => MapToBLLEntity(entity)).ToList();
        
        protected abstract TDALEntity MapToDALEntity(TBLLEntity entity);
        protected abstract TBLLEntity MapToBLLEntity(TDALEntity entity);
    }
}
