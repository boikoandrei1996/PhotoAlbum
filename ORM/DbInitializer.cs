using System;
using System.Data.Entity;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ORM
{
    public class DbInitializer : 
        //DropCreateDatabaseAlways<EntityContext>
        CreateDatabaseIfNotExists<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            base.Seed(context);

            List<Role> roles = LoadJson<Role>("Roles.json");
            context.Roles.AddRange(roles);

            List<User> users = LoadJson<User>("Users.json");
            context.Users.AddRange(users);
            context.SaveChanges();

            List<Photo> photos = LoadJson<Photo>("Photos.json");
            context.Photos.AddRange(photos);
            context.SaveChanges();

            List<Like> likes = LoadJson<Like>("Likes.json");
            context.Likes.AddRange(likes);
            context.SaveChanges();
        }

        private List<T> LoadJson<T>(string fileName)
        {
            string basePath = @"C:\Users\BoikoAndrei\Documents\Visual Studio 2015\Projects\PhotoAlbum\ORM\init_data_files\";
            using (StreamReader stream = new StreamReader(basePath + fileName))
            {
                string json = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }
        }
    }
}
