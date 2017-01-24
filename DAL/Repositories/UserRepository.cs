using System.Data.Entity;
using System.Linq;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using ORM;

namespace DAL.Repositories
{
    public class UserRepository : Repository<DALUser, User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public override void Delete(int id)
        {
            var dbSet = context.Set<Photo>();
            foreach (var photo in dbSet.Where(p => p.UserId == id))
                dbSet.Remove(photo);

            base.Delete(id);
        }

        public DALUser GetByLogin(string login)
        {
            if (login == null)
                return null;

            var entity = dbSet.FirstOrDefault(e => e.Login == login);

            return MapToDALEntity(entity);
        }

        protected override DALUser MapToDALEntity(User entity) => Mapper.Map<User, DALUser>(entity);
        protected override User MapToEntity(DALUser entity) => Mapper.Map<DALUser, User>(entity);
    }
}
