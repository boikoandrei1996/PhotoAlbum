using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class Like : Entity
    {

        [Index("IX_UniqueUserPhotoLike", 1, IsUnique = true)]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Index("IX_UniqueUserPhotoLike", 2, IsUnique = true)]
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
