using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using Logger;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly ILogger logger;
        #endregion

        #region Ctors
        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.userRepository = repository;
            logger = GlobalLogger.Logger;
        }
        #endregion

        #region Public Methods

        public void CreateUser(UserEntity user, RoleEntity role)
        {
            try {
                userRepository.Create(user.ToDalUser(), role.ToDalRole());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Create user with role was failed.", this), ex);
            }
        }

        public void CreateUser(UserEntity user)
        {
            try {
                userRepository.Create(user.ToDalUser());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Create user was failed.", this), ex);
            }
        }

        public void DeleteUser(UserEntity user)
        {
            try { 
                userRepository.Delete(user.ToDalUser());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Delete user was failed.", this), ex);
            }
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            try {
                return userRepository.GetAll().Select(user => user.ToBllUser());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get all user was failed.", this), ex);
            }

            return null;
        }

        public UserEntity GetUserByEmail(string name)
        {
            try {
                return userRepository.GetUserByEmail(name).ToBllUser();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get user by email {0} was failed.", name), this), ex);
            }

            return null;
        }

        public UserEntity GetUserByEmailPassword(string name, string passwd)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserEntity(int id)
        {
            try
            {
                return userRepository.GetById(id).ToBllUser();
            }
            catch(Exception ex)
            {
                logger.Error(logger.GetMessage(string.Format("Get user by id {0} was failed.", id), this), ex);
            }

            return null;
        }

        public void UpdateUser(UserEntity user)
        {
            try {
                userRepository.Update(user.ToDalUser());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Update user with id {0} was failed.", user.UserId), this), ex);
            }
        }

        #endregion
    }
}
