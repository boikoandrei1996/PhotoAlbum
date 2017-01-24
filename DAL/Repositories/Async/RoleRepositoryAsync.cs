using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using DAL.Entities;
using DAL.Interfaces.Async;
using ORM;

namespace DAL.Repositories.Async
{
    public class RoleRepositoryAsync : RepositoryAsync<DALRole, Role>, IRoleRepositoryAsync
    {
        public RoleRepositoryAsync(DbContext context) : base(context) { }

        public async Task<DALRole> GetByNameAsync(string name, CancellationToken? token = null)
        {
            if (name == null)
                return null;

            Role entity;
            if (token == null)
                entity = await dbSet.FirstOrDefaultAsync(e => e.Name == name);
            else
                entity = await dbSet.FirstOrDefaultAsync(e => e.Name == name, token.Value);

            return MapToDALEntity(entity);
        }

        protected override DALRole MapToDALEntity(Role entity) => Mapper.Map<Role, DALRole>(entity);
        protected override Role MapToEntity(DALRole entity) => Mapper.Map<DALRole, Role>(entity);
    }
}
