using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using DAL.Interfaces.Async;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces.Async;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services.Async
{
    public class ProfileServiceAsync : ServiceAsync<BLLProfile, DALProfile>, IProfileServiceAsync
    {
        private readonly IProfileRepositoryAsync repositoryProfile;
        public ProfileServiceAsync(IUnitOfWorkAsync _context) : base(_context, _context.Profiles)
        {
            repositoryProfile = context.Profiles;
        }
        public async Task<ICollection<BLLProfile>> GetAllByNameAsync(string firstOrLastName, CancellationToken? token = null)
        {
            var entities = await repositoryProfile.GetAllByNameAsync(firstOrLastName, token);

            return entities.Select(e => MapToBLLEntity(e)).ToList();
        }
        
        protected override BLLProfile MapToBLLEntity(DALProfile entity) =>
            Mapper.Map<DALProfile, BLLProfile>(entity);
        protected override DALProfile MapToDALEntity(BLLProfile entity) =>
            Mapper.Map<BLLProfile, DALProfile>(entity);
    }
}
