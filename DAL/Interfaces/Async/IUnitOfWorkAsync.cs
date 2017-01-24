using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces.Async
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        IUserRepositoryAsync Users { get; }
        IProfileRepositoryAsync Profiles { get; }
        IPhotoRepositoryAsync Photos { get; }
        ILikeRepositoryAsync Likes { get; }
        IRoleRepositoryAsync Roles { get; }
        Task CommitAsync(CancellationToken? token = null);
        //void Rollback();
    }
}
