using System.Data.Entity;

namespace DAL.Interfaces
{
    public interface IRepositoryFactory
    {
        IUserRepository CreateUserRepository(DbContext context);
        IProfileRepository CreateProfileRepository(DbContext context);
        IPhotoRepository CreatePhotoRepository(DbContext context);
        ILikeRepository CreateLikeRepository(DbContext context);
        IRoleRepository CreateRoleRepository(DbContext context);
    }
}
