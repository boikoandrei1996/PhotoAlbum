using System;

namespace BLL.Entities
{
    public class BLLProfile : BLLEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public byte[] AvatarPhoto { get; set; }
        public string AvatarMimeType { get; set; }
    }
}
