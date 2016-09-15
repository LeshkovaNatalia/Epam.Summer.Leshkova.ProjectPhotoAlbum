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

        #region Public Methods

        /// <summary>
        /// Method CreateRole create role entity.
        /// </summary>
        /// <param name="dalRole">New role entity for create.</param>
        public void CreateRole(RoleEntity role)
        {
            try {
                roleRepository.Create(role.ToDalRole());
                uow.Commit();
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Create role was failed.", this), ex);
            }
        }

        /// <summary>
        /// Method GetAllRoles return all roles.
        /// </summary>
        /// <returns>Lists of roles.</returns>
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

        /// <summary>
        /// Method GetRoleEntity return RoleEntity entity by id.
        /// </summary>
        /// <param name="id">Id of role entity.</param>
        /// <returns>RoleEntity entity by id.</returns>
        public RoleEntity GetRoleEntity(int id)
        {
            try {
                return roleRepository.GetById(id).ToBllRole();
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Get role by id was failed.", this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method DeleteRole delete role. 
        /// </summary>
        /// <param name="dalRole">Entity that need delete.</param>
        public void DeleteRole(RoleEntity role)
        {
            try
            {
                roleRepository.Delete(role.ToDalRole());
                uow.Commit();
            }
            catch (Exception ex)
            {
                logger.Error(logger.GetMessage("Delete role was failed.", this), ex);
            }
        }

        #endregion
    }
}
