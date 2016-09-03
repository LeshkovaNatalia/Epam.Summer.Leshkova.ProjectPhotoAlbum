using DAL.Interface.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DALEntityMappers
    {
        public static ICollection<DalRole> ToDalRoles(this ICollection<Role> roles)
        {
            ICollection<DalRole> dalRoles = new HashSet<DalRole>();
            foreach (var role in roles)
                dalRoles.Add(new DalRole { Name = role.Name });

            return dalRoles;
        }

        public static DalRole ToDalRole(this ICollection<Role> roles)
        {
            DalRole dalRoles = new DalRole();
            foreach (var role in roles)
                dalRoles = new DalRole { Name = role.Name };

            return dalRoles;
        }

        public static DalRole ToDalRole(this Role role)
        {
            DalRole dalRole = new DalRole { Name = role.Name };

            return dalRole;
        }

        public static ICollection<Role> ToRoles(this ICollection<DalRole> dalRoles)
        {
            ICollection<Role> roles = new HashSet<Role>();
            foreach (var role in dalRoles)
                roles.Add(new Role { Name = role.Name, RoleId = role.Id });

            return roles;
        }
    }
}
