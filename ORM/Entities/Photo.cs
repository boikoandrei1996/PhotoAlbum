using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ORM.Infrastructure;

namespace ORM
{
    public class Photo : Entity
    {
        public Photo()
        {
            Likes = new List<Like>();
        }

        [Required]
        [Column(TypeName = "image")]
        [Newtonsoft.Json.JsonConverter(typeof(ImageJsonConverter))]
        public byte[] Content { get; set; }

        [Required]
        [MaxLength(10)]
        public string ContentMimeType { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
