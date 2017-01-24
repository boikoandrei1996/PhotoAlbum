using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class DALLike : DALEntity
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }
    }
}
