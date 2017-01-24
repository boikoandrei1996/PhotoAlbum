using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Entities;
using DAL.Interfaces.Async;
using ORM;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.Async
{
    public abstract class RepositoryAsync<TDALEntity, TEntity> : IRepositoryAsync<TDALEntity>
        where TDALEntity : DALEntity
        where TEntity : Entity
    {
        protected readonly DbContext context;
        protected readonly IDbSet<TEntity> dbSet;

        public RepositoryAsync(DbContext _context)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            context = _context;
            dbSet = context.Set<TEntity>();
        }

        public async virtual Task CreateAsync(TDALEntity entity, CancellationToken? token = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (token == null)
                await Task.Run(() => dbSet.Add(MapToEntity(entity)));
            else
                await Task.Run(() => dbSet.Add(MapToEntity(entity)), token.Value);
        }
        
        public async virtual Task DeleteAsync(int id, CancellationToken? token = null)
        {
            TEntity entity;
            if (token == null)
                entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id);
            else
                entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id, token.Value);

            if (entity != null)
                dbSet.Remove(entity);
        }
        public async virtual Task<IEnumerable<TDALEntity>> GetAllAsync(CancellationToken? token = null)
        {
            List<TEntity> list;

            if (token == null)
                list = await dbSet.ToListAsync();
            else
                list = await dbSet.ToListAsync(token.Value);

            return list.Select(e => MapToDALEntity(e));
        }
        public async virtual Task<TDALEntity> GetByIdAsync(int id, CancellationToken? token = null)
        {
            TEntity entity;

            if (token == null)
                entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id);
            else
                entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id, token.Value);

            return MapToDALEntity(entity);
        }
        public async virtual Task UpdateAsync(TDALEntity entity, CancellationToken? token = null)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TEntity entityORM;
            if (token == null)
                entityORM = await dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
            else
                entityORM = await dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id, token.Value);

            AutoMapper.Mapper.Map(entity, entityORM);
        }

        protected abstract TEntity MapToEntity(TDALEntity entity);
        protected abstract TDALEntity MapToDALEntity(TEntity entity);        
    }
}
