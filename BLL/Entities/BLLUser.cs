using System;
using System.Collections.Generic;

namespace BLL.Entities
{
    public class BLLUser : BLLEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public BLLProfile Profile { get; set; }
    }
}
