using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using AutoMapper;

using DAL.Entities;
using DAL.Interfaces;
using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class UserService : Service<BLLUser, DALUser>, IUserService
    {
        private readonly IUserRepository repositoryUser;
        public UserService(IUnitOfWork _context) : base(_context, _context.Users)
        {
            repositoryUser = context.Users;
        }

        public bool CheckPassword(string login, string password)
        {
            var user = repositoryUser.GetByLogin(login);
            if (user == null)
                return false;

            return Crypto.VerifyHashedPassword(user.Password, password);
        }
        public BLLUser GetByLogin(string login) => MapToBLLEntity(repositoryUser.GetByLogin(login));
        public BLLUser GetPhotoUser(BLLPhoto photo)
        {
            if (photo == null)
                return null;

            var user = repositoryUser.GetById(photo.UserId);

            return MapToBLLEntity(user);
        }
        public ICollection<BLLUser> GetUsersWithRole(BLLRole role)
        {
            if (role == null)
                return new List<BLLUser>();

            var users = repositoryUser.GetAll().Where(user => user.RoleId == role.Id).Select(e => MapToBLLEntity(e));

            return users.ToList();
        }

        protected override BLLUser MapToBLLEntity(DALUser entity) =>
            Mapper.Map<DALUser, BLLUser>(entity);
        protected override DALUser MapToDALEntity(BLLUser entity) =>
            Mapper.Map<BLLUser, DALUser>(entity);
    }
}
