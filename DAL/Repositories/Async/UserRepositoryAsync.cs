using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using ORM;

namespace DAL.Repositories.Async
{
    public class UserRepositoryAsync : RepositoryAsync<DALUser, User>, IUserRepositoryAsync
    {
        public UserRepositoryAsync(DbContext context) : base(context) { }

        public override async Task DeleteAsync(int id, CancellationToken? token = null)
        {
            var dbSet = context.Set<Photo>();

            await Task.Run(() =>
            {
                foreach (var photo in dbSet.Where(p => p.UserId == id))
                    dbSet.Remove(photo);
            });
            
            await base.DeleteAsync(id, token);
        }

        public async Task<DALUser> GetByLoginAsync(string login, CancellationToken? token = null)
        {
            if (login == null)
                return null;

            User entity;
            if (token == null)
                entity = await dbSet.FirstOrDefaultAsync(e => e.Login == login);
            else
                entity = await dbSet.FirstOrDefaultAsync(e => e.Login == login, token.Value);

            return MapToDALEntity(entity);
        }

        protected override DALUser MapToDALEntity(User entity) => Mapper.Map<User, DALUser>(entity);
        protected override User MapToEntity(DALUser entity) => Mapper.Map<DALUser, User>(entity);
    }
}
