using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public abstract class Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
