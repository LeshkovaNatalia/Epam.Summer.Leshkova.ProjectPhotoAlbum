using BLL.Interface.Entities;
using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {

        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string email, string roleName)
        {

            UserEntity user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Email == email);

            if (user == null)
                return false;
            if(user.Roles.Contains(new RoleEntity() { Name = roleName}))
                return true;

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };
            var user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Email == email);

            if (user == null)
                return roles;

            var userRoles = user.Roles;

            if (userRoles != null)
            {
                roles = new string[userRoles.Count];
                int i = 0;
                foreach (var role in userRoles)
                {
                    roles[i] = role.Name;
                    i++;
                }
            }

            return roles;
        }

        public override void CreateRole(string roleName)
        {
            if (RoleExists(roleName) == false)
            {
                var newRole = new RoleEntity() { Name = roleName };
                RoleService.CreateRole(newRole);
            }
        }

        #region Stabs

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

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var existRole = RoleService.GetAllRoles().FirstOrDefault(r => r.Name == roleName);

            if (existRole != null)
                return true;

            return false;
        }

        #endregion
    }
}