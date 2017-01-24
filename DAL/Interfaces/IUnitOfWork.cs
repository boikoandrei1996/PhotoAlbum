using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProfileRepository Profiles { get; }
        IPhotoRepository Photos { get; }
        ILikeRepository Likes { get; }
        IRoleRepository Roles { get; }
        void Commit();
        //void Rollback();
    }
}
