using System;

using BLL.Entities;
using DAL.Entities;
using ORM;

namespace Mapping.Profiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Profile, DALProfile>();
            CreateMap<DALProfile, Profile>();

            CreateMap<DALProfile, BLLProfile>();
            CreateMap<BLLProfile, DALProfile>();
        }
    }
}