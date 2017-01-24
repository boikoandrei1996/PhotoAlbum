using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DAL.Entities;
using DAL.Interfaces.Async;
using BLL.Entities;
using BLL.Interfaces.Async;

namespace BLL.Services.Async
{
    public abstract class ServiceAsync<TBLLEntity, TDALEntity> : IServiceAsync<TBLLEntity>
        where TBLLEntity : BLLEntity
        where TDALEntity : DALEntity
    {
        protected readonly IUnitOfWorkAsync context;
        private readonly IRepositoryAsync<TDALEntity> repositoryBase;
        public ServiceAsync(IUnitOfWorkAsync _context, IRepositoryAsync<TDALEntity> _repository)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            if (_repository == null)
                throw new ArgumentNullException(nameof(_repository));

            context = _context;
            repositoryBase = _repository;
        }
        
        public async virtual Task SaveAsync(TBLLEntity entity, CancellationToken? token = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id < 0)
                throw new ArgumentOutOfRangeException(nameof(entity.Id));

            if (entity.Id == 0)
                await repositoryBase.CreateAsync(MapToDALEntity(entity), token);
            else
                await repositoryBase.UpdateAsync(MapToDALEntity(entity), token);

            await context.CommitAsync(token);
        }
        public async virtual Task DeleteAsync(int id, CancellationToken? token = null)
        {
            await repositoryBase.DeleteAsync(id, token);
            await context.CommitAsync(token);
        }
        public async virtual Task<TBLLEntity> GetByIdAsync(int id, CancellationToken? token = null)
        {
            TDALEntity entity = await repositoryBase.GetByIdAsync(id, token);
            return MapToBLLEntity(entity);
        }
        public async virtual Task<ICollection<TBLLEntity>> GetAllAsync(CancellationToken? token = null)
        {
            IEnumerable<TDALEntity> entities = await repositoryBase.GetAllAsync(token);
            return entities.Select(entity => MapToBLLEntity(entity)).ToList();
        }
        
        protected abstract TDALEntity MapToDALEntity(TBLLEntity entity);
        protected abstract TBLLEntity MapToBLLEntity(TDALEntity entity);
    }
}
