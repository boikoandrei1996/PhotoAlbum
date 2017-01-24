using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class RoleService : Service<BLLRole, DALRole>, IRoleService
    {
        private readonly IRoleRepository repositoryRole;
        public RoleService(IUnitOfWork _context) : base(_context, _context.Roles)
        {
            repositoryRole = context.Roles;
        }

        public BLLRole GetByName(string name) => MapToBLLEntity(repositoryRole.GetByName(name));
        public BLLRole GetUserRole(BLLUser user)
        {
            if (user == null)
                return null;

            var role = repositoryRole.GetById(user.RoleId);

            return MapToBLLEntity(role);
        }
        
        protected override BLLRole MapToBLLEntity(DALRole entity) =>
            Mapper.Map<DALRole, BLLRole>(entity);
        protected override DALRole MapToDALEntity(BLLRole entity) =>
            Mapper.Map<BLLRole, DALRole>(entity);
    }
}
