using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using AutoMapper;

using DAL.Entities;
using DAL.Interfaces.Async;
using BLL.Entities;
using BLL.Interfaces.Async;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services.Async
{
    public class UserServiceAsync : ServiceAsync<BLLUser, DALUser>, IUserServiceAsync
    {
        private readonly IUserRepositoryAsync repositoryUser;
        public UserServiceAsync(IUnitOfWorkAsync _context) : base(_context, _context.Users)
        {
            repositoryUser = context.Users;
        }
        
        public async Task<bool> CheckPasswordAsync(string login, string password, CancellationToken? token = null)
        {
            var user = await repositoryUser.GetByLoginAsync(login, token);
            if (user == null)
                return false;

            return Crypto.VerifyHashedPassword(user.Password, password);
        }
        public async Task<BLLUser> GetByLoginAsync(string login, CancellationToken? token = null)
        {
            DALUser user = await repositoryUser.GetByLoginAsync(login, token);
            return MapToBLLEntity(user);
        }
        public async Task<BLLUser> GetPhotoUserAsync(BLLPhoto photo, CancellationToken? token = null)
        {
            if (photo == null)
                return null;

            var user = await repositoryUser.GetByIdAsync(photo.UserId, token);

            return MapToBLLEntity(user);
        }
        public async Task<ICollection<BLLUser>> GetUsersWithRoleAsync(BLLRole role, CancellationToken? token = null)
        {
            if (role == null)
                return new List<BLLUser>();

            var allUsers = await repositoryUser.GetAllAsync(token);
            var users = allUsers.Where(user => user.RoleId == role.Id).Select(e => MapToBLLEntity(e));

            return users.ToList();
        }
        
        protected override BLLUser MapToBLLEntity(DALUser entity) =>
            Mapper.Map<DALUser, BLLUser>(entity);
        protected override DALUser MapToDALEntity(BLLUser entity) =>
            Mapper.Map<BLLUser, DALUser>(entity);
    }
}
