using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class DALPhoto : DALEntity
    {
        public byte[] Content { get; set; }
        public string ContentMimeType { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
    }
}
