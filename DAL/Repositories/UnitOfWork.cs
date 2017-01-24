using System;
using System.Data.Entity;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        #region private fields
        private bool isDisposed = false;
        private readonly DbContext context;
        
        private readonly IRepositoryFactory factory;
        private IUserRepository userRepository;
        private IProfileRepository profileRepository;
        private IPhotoRepository photoRepository;
        private ILikeRepository likeRepository;
        private IRoleRepository roleRepository;
                
        #endregion

        public EFUnitOfWork(DbContext _context, IRepositoryFactory _factory)
        {
            if (_context == null)
                throw new ArgumentNullException(nameof(_context));

            if (_factory == null)
                throw new ArgumentNullException(nameof(_factory));

            context = _context;
            factory = _factory;
        }

        #region repositories property
        public ILikeRepository Likes
        {
            get
            {
                if (likeRepository == null)
                    likeRepository = factory.CreateLikeRepository(context);

                return likeRepository;
            }
        }
        public IPhotoRepository Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = factory.CreatePhotoRepository(context);

                return photoRepository;
            }
        }
        public IProfileRepository Profiles
        {
            get
            {
                if (profileRepository == null)
                    profileRepository = factory.CreateProfileRepository(context);

                return profileRepository;
            }
        }
        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = factory.CreateRoleRepository(context);

                return roleRepository;
            }
        }
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = factory.CreateUserRepository(context);

                return userRepository;
            }
        }
        #endregion

        public void Commit() => context.SaveChanges();

        ~EFUnitOfWork()
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
