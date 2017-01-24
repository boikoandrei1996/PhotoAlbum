using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using DAL.Interfaces;
using DAL.Entities;
using BLL.Entities;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ProfileService : Service<BLLProfile, DALProfile>, IProfileService
    {
        private readonly IProfileRepository repositoryProfile;
        public ProfileService(IUnitOfWork _context) : base(_context, _context.Profiles)
        {
            repositoryProfile = context.Profiles;
        }

        public ICollection<BLLProfile> GetAllByName(string firstOrLastName) =>
            repositoryProfile.GetAllByName(firstOrLastName).Select(e => MapToBLLEntity(e)).ToList();

        protected override BLLProfile MapToBLLEntity(DALProfile entity) =>
            Mapper.Map<DALProfile, BLLProfile>(entity);
        protected override DALProfile MapToDALEntity(BLLProfile entity) =>
            Mapper.Map<BLLProfile, DALProfile>(entity);
    }
}
