using System;
using AutoMapper;

using BLL.Entities;
using DAL.Entities;
using ORM;

namespace Mapping.Profiles
{
    public class RoleProfile : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, DALRole>();
            CreateMap<DALRole, Role>();

            CreateMap<DALRole, BLLRole>();
            CreateMap<BLLRole, DALRole>();
        }
    }
}
