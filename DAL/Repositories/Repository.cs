using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Entities;
using DAL.Interfaces;
using ORM;
using System.Data.Entity;

namespace DAL.Repositories
{
    public abstract class Repository<TDALEntity, TEntity> : IRepository<TDALEntity>
        where TDALEntity : DALEntity
        where TEntity : Entity
    {
        protected readonly DbContext context;
        protected readonly IDbSet<TEntity> dbSet;

        public Repository(DbContext _context)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            context = _context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Create(TDALEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Add(MapToEntity(entity));
        }
        public virtual void Delete(int id)
        {
            var entity = dbSet.FirstOrDefault(e => e.Id == id);
            if (entity != null)
                dbSet.Remove(entity);
        }
        public virtual IEnumerable<TDALEntity> GetAll()
        {
            var list = dbSet.ToList();

            return list.Select(e => MapToDALEntity(e));
        }
        public virtual TDALEntity GetById(int id)
        {
            var entity = dbSet.FirstOrDefault(e => e.Id == id);

            return MapToDALEntity(entity);
        }
        public virtual void Update(TDALEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityORM = dbSet.FirstOrDefault(e => e.Id == entity.Id);           
            AutoMapper.Mapper.Map(entity, entityORM);
        }

        protected abstract TEntity MapToEntity(TDALEntity entity);
        protected abstract TDALEntity MapToDALEntity(TEntity entity);        
    }
}
