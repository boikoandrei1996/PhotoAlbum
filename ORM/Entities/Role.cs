using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Required]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
