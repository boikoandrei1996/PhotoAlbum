using System;
using Ninject.Modules;
using Ninject.Extensions.Factory;

using DAL.Interfaces;
using DAL.Repositories;
using DAL.Interfaces.Async;
using DAL.Repositories.Async;

using BLL.Interfaces;
using BLL.Services;
using BLL.Interfaces.Async;
using BLL.Services.Async;

using ORM;
using System.Data.Entity;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        private readonly string connectionString;
        public ResolverModule(string _connectionString)
        {
            if (_connectionString == null)
                throw new ArgumentNullException(nameof(_connectionString));

            connectionString = _connectionString;
        }

        public override void Load()
        {
            Bind<DbContext>().To<EntityContext>().WithConstructorArgument(connectionString);

            BindSyncClasses();
            BindAsyncClasses();
        }

        private void BindSyncClasses()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IProfileRepository>().To<ProfileRepository>();
            Bind<IPhotoRepository>().To<PhotoRepository>();
            Bind<ILikeRepository>().To<LikeRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IRepositoryFactory>().ToFactory();

            Bind<IUnitOfWork>().To<EFUnitOfWork>();

            Bind<IUserService>().To<UserService>();
            Bind<IProfileService>().To<ProfileService>();
            Bind<IPhotoService>().To<PhotoService>();
            Bind<ILikeService>().To<LikeService>();
            Bind<IRoleService>().To<RoleService>();
        }

        private void BindAsyncClasses()
        {
            Bind<IUserRepositoryAsync>().To<UserRepositoryAsync>();
            Bind<IProfileRepositoryAsync>().To<ProfileRepositoryAsync>();
            Bind<IPhotoRepositoryAsync>().To<PhotoRepositoryAsync>();
            Bind<ILikeRepositoryAsync>().To<LikeRepositoryAsync>();
            Bind<IRoleRepositoryAsync>().To<RoleRepositoryAsync>();
            Bind<IRepositoryFactoryAsync>().ToFactory();

            Bind<IUnitOfWorkAsync>().To<EFUnitOfWorkAsync>();

            Bind<IUserServiceAsync>().To<UserServiceAsync>();
            Bind<IProfileServiceAsync>().To<ProfileServiceAsync>();
            Bind<IPhotoServiceAsync>().To<PhotoServiceAsync>();
            Bind<ILikeServiceAsync>().To<LikeServiceAsync>();
            Bind<IRoleServiceAsync>().To<RoleServiceAsync>();
        }        
    }
}
