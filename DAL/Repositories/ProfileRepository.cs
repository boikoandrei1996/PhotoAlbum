using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ProfileRepository : Repository<DALProfile, ORM.Profile>, IProfileRepository
    {
        public ProfileRepository(DbContext context) : base(context) { }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DALProfile> GetAllByName(string firstOrLastName)
        {
            if (firstOrLastName == null)
                return new List<DALProfile>();

            var entities = dbSet.Where(
                e => e.FirstName == firstOrLastName || e.LastName == firstOrLastName
            ).ToList();

            return entities.Select(e => MapToDALEntity(e));
        }

        protected override DALProfile MapToDALEntity(ORM.Profile entity) => Mapper.Map<ORM.Profile, DALProfile>(entity);
        protected override ORM.Profile MapToEntity(DALProfile entity) => Mapper.Map<DALProfile, ORM.Profile>(entity);
    }
}
