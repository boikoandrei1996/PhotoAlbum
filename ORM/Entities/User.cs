using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ORM.Infrastructure;

namespace ORM
{
    public class User : Entity
    {
        public User()
        {
            Photos = new List<Photo>();
            Likes = new List<Like>();
        }
        
        [Required]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Login { get; set; }

        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(PasswordJsonConverter))]
        public string Password { get; set; } //MD5 hash

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Required]
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
 