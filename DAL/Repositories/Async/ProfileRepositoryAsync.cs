using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;

namespace DAL.Repositories.Async
{
    public class ProfileRepositoryAsync : RepositoryAsync<DALProfile, ORM.Profile>, IProfileRepositoryAsync
    {
        public ProfileRepositoryAsync(DbContext context) : base(context) { }

        public override Task DeleteAsync(int id, CancellationToken? token = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<DALProfile>> GetAllByNameAsync(string firstOrLastName, CancellationToken? token = null)
        {
            if (firstOrLastName == null)
                return new List<DALProfile>();

            var entities = dbSet.Where(
                e => e.FirstName == firstOrLastName || e.LastName == firstOrLastName
            );

            List<ORM.Profile> profiles;
            if (token == null)
                profiles = await entities.ToListAsync();
            else
                profiles = await entities.ToListAsync(token.Value);

            return profiles.Select(e => MapToDALEntity(e));
        }

        protected override DALProfile MapToDALEntity(ORM.Profile entity) => Mapper.Map<ORM.Profile, DALProfile>(entity);
        protected override ORM.Profile MapToEntity(DALProfile entity) => Mapper.Map<DALProfile, ORM.Profile>(entity);
    }
}
