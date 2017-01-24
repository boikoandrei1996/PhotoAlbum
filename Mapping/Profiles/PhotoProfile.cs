using System;
using AutoMapper;

using BLL.Entities;
using DAL.Entities;
using ORM;

namespace Mapping.Profiles
{
    public class PhotoProfile : AutoMapper.Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, DALPhoto>();
            CreateMap<DALPhoto, Photo>();

            CreateMap<DALPhoto, BLLPhoto>();
            CreateMap<BLLPhoto, DALPhoto>();
        }
    }
}
