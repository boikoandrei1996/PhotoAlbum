using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ORM.Infrastructure;

namespace ORM
{
    public class Profile : Entity
    {
        [Key, ForeignKey("User")]
        public override int Id { get; set; }
        public virtual User User { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "image")]
        [Newtonsoft.Json.JsonConverter(typeof(ImageJsonConverter))]
        public byte[] AvatarPhoto { get; set; }  
        
        [MaxLength(10)]
        public string AvatarMimeType { get; set; }      
    }

    
}
