using System;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces.Async;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services.Async
{
    public class RoleServiceAsync : ServiceAsync<BLLRole, DALRole>, IRoleServiceAsync
    {
        private readonly IRoleRepositoryAsync repositoryRole;
        public RoleServiceAsync(IUnitOfWorkAsync _context) : base(_context, _context.Roles)
        {
            repositoryRole = context.Roles;
        }
        
        public async Task<BLLRole> GetByNameAsync(string name, CancellationToken? token = null)
        {
            var role = await repositoryRole.GetByNameAsync(name, token);
            return MapToBLLEntity(role);
        }
        public async Task<BLLRole> GetUserRoleAsync(BLLUser user, CancellationToken? token = null)
        {
            if (user == null)
                return null;

            var role = await repositoryRole.GetByIdAsync(user.RoleId, token);

            return MapToBLLEntity(role);
        }
        
        protected override BLLRole MapToBLLEntity(DALRole entity) =>
            Mapper.Map<DALRole, BLLRole>(entity);
        protected override DALRole MapToDALEntity(BLLRole entity) =>
            Mapper.Map<BLLRole, DALRole>(entity);
    }
}
