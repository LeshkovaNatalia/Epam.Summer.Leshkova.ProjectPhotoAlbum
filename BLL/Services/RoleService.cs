using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using Logger;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalRole> roleRepository;
        private readonly ILogger logger;
        #endregion

        #region Ctors
        public RoleService(IUnitOfWork uow, IRepository<DalRole> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
            logger = GlobalLogger.Logger;
        }
        #endregion

        public void CreateRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            try {
                return roleRepository.GetAll().Select(role => role.ToBllRole());
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Get all roles was failed.", this), ex);
            }

            return null;
        }

        public RoleEntity GetRoleEntity(int id)
        {
            throw new NotImplementedException();
        }

        public string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleEntity role)
        {
            throw new NotImplementedException();
        }
    }
}
