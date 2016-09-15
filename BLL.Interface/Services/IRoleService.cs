using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        RoleEntity GetRoleEntity(int id);
        IEnumerable<RoleEntity> GetAllRoles();
        void CreateRole(RoleEntity role);
        void DeleteRole(RoleEntity role);
    }
}
