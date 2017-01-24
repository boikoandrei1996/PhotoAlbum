using System.Data.Entity;

namespace DAL.Interfaces.Async
{
    public interface IRepositoryFactoryAsync
    {
        IUserRepositoryAsync CreateUserRepository(DbContext context);
        IProfileRepositoryAsync CreateProfileRepository(DbContext context);
        IPhotoRepositoryAsync CreatePhotoRepository(DbContext context);
        ILikeRepositoryAsync CreateLikeRepository(DbContext context);
        IRoleRepositoryAsync CreateRoleRepository(DbContext context);
    }
}
