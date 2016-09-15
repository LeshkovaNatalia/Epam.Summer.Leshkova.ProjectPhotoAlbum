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

        /// <summary>
        /// Method CreateUser create user entity.
        /// </summary>
        /// <param name="user">New user entity for create.</param>
        /// <param name="role">Role for new user.</param>
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

        /// <summary>
        /// Method CreateUser create user entity.
        /// </summary>
        /// <param name="user">New user entity for create.</param>
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

        /// <summary>
        /// Method DeleteUser user.
        /// </summary>
        /// <param name="user">Entity that need delete.</param>
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

        /// <summary>
        /// Method return all users.
        /// </summary>
        /// <returns>Lists of users entity.</returns>
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

        /// <summary>
        /// Method return user entity by email.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <returns>User entity by email.</returns>
        public UserEntity GetUserByEmail(string email)
        {
            try {
                return userRepository.GetUserByEmail(email).ToBllUser();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get user by email {0} was failed.", email), this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method GetUserEntity return UserEntity by id.
        /// </summary>
        /// <param name="id">Id of user entity.</param>
        /// <returns>UserEntity entity by id.</returns>
        public UserEntity GetUserEntity(int id)
        {
            try {
                return userRepository.GetById(id).ToBllUser();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get user by id {0} was failed.", id), this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method UpdateUser update exists user.
        /// </summary>
        /// <param name="user">UserEntity that need update.</param>
        public void UpdateUser(UserEntity user)
        {
            try {
                userRepository.Update(user.ToDalUser());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Update user with id {0} was failed.", user.UserId), this), ex);
            }
        }

        /// <summary>
        /// Method return roles for user in string format.
        /// </summary>
        /// <param name="userId">IUser's id.</param>
        /// <returns>Roles for user.</returns>
        public string GetRolesForUser(int userId)
        {
            try  {
                var roles = userRepository.GetById(userId).Roles;
                string result = string.Empty;
                int i = 0;
                foreach (var role in roles)
                {
                    result += role.Name;
                    result += " ";
                    i++;
                }
                return result;
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("GetRolesForUser user with id {0} was failed.", userId), this), ex);
            }

            return null;
        }

        #endregion
    }
}
