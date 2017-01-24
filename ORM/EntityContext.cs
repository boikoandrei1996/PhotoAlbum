using System.Data.Entity;
using System.Configuration;

namespace ORM
{
    public class EntityContext : DbContext
    {
        static EntityContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        //public EntityContext() : 
        //    base(ConfigurationManager.ConnectionStrings["EntityContextConnection"]?.ConnectionString) { }
        
        public EntityContext(string connectionString) : base(connectionString) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasRequired(p => p.User)
                .WithMany(u => u.Photos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profile>()
                .HasRequired(p => p.User)
                .WithRequiredDependent(u => u.Profile)
                .WillCascadeOnDelete(true);
        }
    }
}
