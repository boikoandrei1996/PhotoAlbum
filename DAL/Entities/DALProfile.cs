using System;

namespace DAL.Entities
{
    public class DALProfile : DALEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public byte[] AvatarPhoto { get; set; }
        public string AvatarMimeType { get; set; }
    }
}
