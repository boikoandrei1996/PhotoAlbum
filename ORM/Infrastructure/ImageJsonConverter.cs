using System;
using Newtonsoft.Json;

namespace ORM.Infrastructure
{
    public class ImageJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(string);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string basePath = @"C:\Users\BoikoAndrei\Documents\Visual Studio 2015\Projects\PhotoAlbum\ORM\init_images\";

            return System.IO.File.ReadAllBytes(basePath + reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
