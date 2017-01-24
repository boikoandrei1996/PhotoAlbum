using System.Data.Entity;
using System.Linq;
using AutoMapper;

using DAL.Entities;
using DAL.Interfaces;
using ORM;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<DALRole, Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context) { }

        public DALRole GetByName(string name)
        {
            if (name == null)
                return null;

            var entity = dbSet.FirstOrDefault(e => e.Name == name);

            return MapToDALEntity(entity);
        }

        protected override DALRole MapToDALEntity(Role entity) => Mapper.Map<Role, DALRole>(entity);
        protected override Role MapToEntity(DALRole entity) => Mapper.Map<DALRole, Role>(entity);
    }
}
