using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Interfaces.Async;

namespace DAL.Repositories.Async
{
    public class EFUnitOfWorkAsync : IUnitOfWorkAsync
    {
        #region private fields
        private bool isDisposed = false;
        private readonly DbContext context;
        
        private readonly IRepositoryFactoryAsync factory;
        private IUserRepositoryAsync userRepository;
        private IProfileRepositoryAsync profileRepository;
        private IPhotoRepositoryAsync photoRepository;
        private ILikeRepositoryAsync likeRepository;
        private IRoleRepositoryAsync roleRepository;
        
        #endregion

        public EFUnitOfWorkAsync(DbContext _context, IRepositoryFactoryAsync _factory)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            if (_factory == null)
                throw new ArgumentNullException(nameof(_factory));

            context = _context;
            factory = _factory;
        }

        #region repositories property
        public ILikeRepositoryAsync Likes
        {
            get
            {
                if (likeRepository == null)
                    likeRepository = factory.CreateLikeRepository(context);

                return likeRepository;
            }
        }
        public IPhotoRepositoryAsync Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = factory.CreatePhotoRepository(context);

                return photoRepository;
            }
        }
        public IProfileRepositoryAsync Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = factory.CreateProfileRepository(context);

                return profileRepository;
            }
        }
        public IRoleRepositoryAsync Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = factory.CreateRoleRepository(context);

                return roleRepository;
            }
        }
        public IUserRepositoryAsync Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = factory.CreateUserRepository(context);

                return userRepository;
            }
        }
        #endregion
        
        public async Task CommitAsync(CancellationToken? token = null)
        {
            if (token == null)
                await context.SaveChangesAsync();
            else
                await context.SaveChangesAsync(token.Value);
        }

        ~EFUnitOfWorkAsync()
        {
            Dispose(false);
        }              
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
                context?.Dispose();

            isDisposed = true;
        }
    }
}
