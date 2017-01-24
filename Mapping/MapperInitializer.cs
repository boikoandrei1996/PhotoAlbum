using System;
using AutoMapper;
using Mapping.Profiles;


namespace Mapping
{
    public static class MapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<UserProfile>();
                config.AddProfile<ProfileProfile>();
                config.AddProfile<PhotoProfile>();
                config.AddProfile<LikeProfile>();
                config.AddProfile<RoleProfile>();
            });
        }
    }
}
