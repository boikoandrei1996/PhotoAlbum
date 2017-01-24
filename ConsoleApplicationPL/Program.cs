using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using BLL.Services;
using BLL.Interfaces;
using BLL.Services.Async;
using BLL.Interfaces.Async;
using BLL.Entities;
using System.Data.Entity;
using Ninject;
using DependencyResolver;
using Mapping;
using NLog;

namespace ConsoleApplicationPL
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        { 
            MapperInitializer.Initialize();

            var kernel = new StandardKernel();
            kernel.Load(new ResolverModule("EntityContextConnection"));

            //TestDeleteMethods(kernel);            
            //TestDeleteAsyncMethods(kernel).Wait();
            //TestDeleteRole(kernel.Get<IRoleService>());

            Console.ReadLine();
        }


        #region Test Delete Methods
        #region Sync verions
        private static void TestDeleteMethods(StandardKernel kernel)
        {
            var userService = kernel.Get<IUserService>();
            var profileService = kernel.Get<IProfileService>();
            var roleService = kernel.Get<IRoleService>();
            var photoService = kernel.Get<IPhotoService>();
            var likeService = kernel.Get<ILikeService>();

            ShowInfo(userService, profileService, likeService, roleService);

            userService.Delete(3);

            try
            {
                profileService.Delete(2);
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("It's okay");
            }

            ShowInfo(userService, profileService, likeService, roleService);

            photoService.Delete(3);

            ShowInfo(userService, profileService, likeService, roleService);

            likeService.Delete(2);

            ShowInfo(userService, profileService, likeService, roleService);
        }
        private static void ShowInfo(IUserService userService, 
                                     IProfileService profileService, 
                                     ILikeService likeService, 
                                     IRoleService roleService)
        {
            Console.WriteLine("===================================");
            foreach (var user in userService.GetAll())
                Console.WriteLine($"{user.Id} login: '{user.Login}' password: '{userService.CheckPassword(user.Login, "10_Messi")}' --- {roleService.GetUserRole(user).Name}");
            foreach (var profile in profileService.GetAll())
                Console.WriteLine($"name: '{profile.FirstName} {profile.LastName}'");
            foreach (var like in likeService.GetAll())
                Console.WriteLine($"Like: {like.UserId} -> {like.PhotoId}");
            Console.WriteLine("===================================");
        }
        #endregion

        #region Async versions
        private async static Task TestDeleteAsyncMethods(StandardKernel kernel)
        {
            var userServiceAsync = kernel.Get<IUserServiceAsync>();
            var profileServiceAsync = kernel.Get<IProfileServiceAsync>();
            var roleServiceAsync = kernel.Get<IRoleServiceAsync>();
            var photoServiceAsync = kernel.Get<IPhotoServiceAsync>();
            var likeServiceAsync = kernel.Get<ILikeServiceAsync>();

            await ShowInfoAsync(userServiceAsync, profileServiceAsync, likeServiceAsync, roleServiceAsync);

            await userServiceAsync.DeleteAsync(3);
            try
            {
                await profileServiceAsync.DeleteAsync(2);
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("It's okay");
            }

            await ShowInfoAsync(userServiceAsync, profileServiceAsync, likeServiceAsync, roleServiceAsync);

            await photoServiceAsync.DeleteAsync(3);

            await ShowInfoAsync(userServiceAsync, profileServiceAsync, likeServiceAsync, roleServiceAsync);

            await likeServiceAsync.DeleteAsync(2);

            await ShowInfoAsync(userServiceAsync, profileServiceAsync, likeServiceAsync, roleServiceAsync);
        }

        private async static Task ShowInfoAsync(IUserServiceAsync userService, 
                                                IProfileServiceAsync profileService, 
                                                ILikeServiceAsync likeService, 
                                                IRoleServiceAsync roleService)
        {
            Console.WriteLine("===================================");
            foreach (var user in await userService.GetAllAsync())
                Console.WriteLine($"{user.Id} login: '{user.Login}' password: '{await userService.CheckPasswordAsync(user.Login, "10_Messi")}' --- {(await roleService.GetUserRoleAsync(user)).Name}");
            foreach (var profile in await profileService.GetAllAsync())
                Console.WriteLine($"name: '{profile.FirstName} {profile.LastName}'");
            foreach (var like in await likeService.GetAllAsync())
                Console.WriteLine($"Like: {like.UserId} -> {like.PhotoId}");
            Console.WriteLine("===================================");
        }
        #endregion
        #endregion

        #region Test Role
        private static void TestDeleteRole(IRoleService roleService)
        {
            ShowRoleInfo(roleService);
            roleService.Save(new BLLRole
            {
                Name = "SomeRole"
            });
            ShowRoleInfo(roleService);
            roleService.Delete(4);
            ShowRoleInfo(roleService);

            try
            {
                roleService.Delete(3);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                Console.WriteLine("You can't delete role, if some user have link to role");
            }
        }
        private static void ShowRoleInfo(IRoleService roleService)
        {
            foreach (var role in roleService.GetAll())
                Console.WriteLine(role.Name);
            Console.WriteLine("===================================");
        }
        #endregion
    }
}
