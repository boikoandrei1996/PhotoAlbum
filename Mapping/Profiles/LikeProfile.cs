using System;
using AutoMapper;

using BLL.Entities;
using DAL.Entities;
using ORM;

namespace Mapping.Profiles
{
    public class LikeProfile : AutoMapper.Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, DALLike>();
            CreateMap<DALLike, Like>();

            CreateMap<DALLike, BLLLike>();
            CreateMap<BLLLike, DALLike>();
        }
    }
}
