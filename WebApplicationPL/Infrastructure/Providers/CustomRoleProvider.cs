using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Security;

using BLL.Interfaces;

namespace WebApplicationPL.Infrastructure.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private IUserService userService;
        private IRoleService roleService;

        public CustomRoleProvider()
        {
            userService = 
                System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService)) as IUserService;
            roleService = 
                System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService)) as IRoleService;
        }

        public override void CreateRole(string roleName)
        {
            int maxLengthRoleName = 20;
            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));

            if (roleName == string.Empty || roleName.Contains(",") || roleName.Length > maxLengthRoleName)
                throw new ArgumentNullException(nameof(roleName));

            if (roleService.GetByName(roleName) != null)
                throw new ProviderException(nameof(roleName));

            roleService.Save(new BLL.Entities.BLLRole { Name = roleName });
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));

            if (roleName == string.Empty)
                throw new ArgumentException(nameof(roleName));

            var role = roleService.GetByName(roleName);
            if (role == null)
                throw new ArgumentException(nameof(roleName));

            var users = userService.GetUsersWithRole(role);
            if (users.Count == 0)
                return false;

            if (throwOnPopulatedRole && users.Count > 0)
                throw new ProviderException(nameof(roleName));

            if (!throwOnPopulatedRole)
                roleService.Delete(role.Id);

            return true;
        }

        public override string[] GetAllRoles()
        {
            List<string> list = new List<string>();
            foreach (var role in roleService.GetAll())
                list.Add(role.Name);

            return list.ToArray();
        }

        public override string[] GetRolesForUser(string login)
        {
            if (login == null )
                throw new ArgumentNullException(nameof(login));

            if (login == string.Empty)
                throw new ArgumentException(nameof(login));

            var user = userService.GetByLogin(login);
            if (user == null)
                return new string[] { };

            var role = roleService.GetUserRole(user);

            return new string[] { role.Name };
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            if (login == null || roleName == null)
                throw new ArgumentNullException($"{nameof(login)} or {nameof(roleName)}");

            if (login == string.Empty || roleName == string.Empty)
                throw new ArgumentException($"{nameof(login)} or {nameof(roleName)}");

            var user = userService.GetByLogin(login);
            if (user == null)
                throw new ProviderException(nameof(login));

            var role = roleService.GetUserRole(user);
            if (role == null)
                throw new ProviderException(nameof(user));

            return role.Name.ToLowerInvariant() == roleName.ToLowerInvariant();
        }

        public override bool RoleExists(string roleName)
        {
            if (roleName == null)
                throw new ArgumentNullException(nameof(roleName));

            if (roleName == string.Empty)
                throw new ArgumentException(nameof(roleName));

            return roleService.GetByName(roleName) != null;
        }

        #region NotImplemented Methods
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }        
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }        
        #endregion
    }
}