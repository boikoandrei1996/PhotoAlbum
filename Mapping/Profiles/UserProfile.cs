using System;
using AutoMapper;

using BLL.Entities;
using DAL.Entities;
using ORM;

namespace Mapping.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, DALUser>();
            CreateMap<DALUser, User>();

            CreateMap<DALUser, BLLUser>();
            CreateMap<BLLUser, DALUser>();
        }
    }
}
