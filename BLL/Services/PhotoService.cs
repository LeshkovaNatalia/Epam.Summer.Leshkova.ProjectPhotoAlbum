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
    public class PhotoService : IPhotoService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalPhoto> photoRepository;
        private readonly ICategoryPhotoRepository categoryRepository;
        private readonly ILogger logger;
        #endregion

        #region Ctors
        public PhotoService(IUnitOfWork uow, IRepository<DalPhoto> repository, ICategoryPhotoRepository categoryRepository)
        {
            this.uow = uow;
            this.photoRepository = repository;
            this.categoryRepository = categoryRepository;
            logger = GlobalLogger.Logger;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method CreatePhoto create photo entity.
        /// </summary>
        /// <param name="photo">New photo entity for create.</param>
        public void CreatePhoto(PhotoEntity photo)
        {
            try {
                photoRepository.Create(photo.ToDalPhoto());
                uow.Commit();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Create photo was failed.", this), ex);
            }
        }

        /// <summary>
        /// Method DeletePhoto delete photo.
        /// </summary>
        /// <param name="photo">Entity that need delete.</param>
        public void DeletePhoto(PhotoEntity photo)
        {
            try {
                photoRepository.Delete(photo.ToDalPhoto());
                uow.Commit();
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Delete photo was failed.", this), ex);
            }
        }

        /// <summary>
        /// Method GetAllPhotoEntities return all photos.
        /// </summary>
        /// <returns>Lists of photos entity.</returns>
        public IEnumerable<PhotoEntity> GetAllPhotoEntities()
        {
            try {
                return photoRepository.GetAll().Select(photo => photo.ToBllPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get all photos was failed.", this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method return all photos for user by userId.
        /// </summary>
        /// <param name="userId">User with userId.</param>
        /// <returns>All photos for user.</returns>
        public IEnumerable<PhotoEntity> GetAllPhotoEntitiesForUser(int userId)
        {
            try {
                return photoRepository.GetAll().Where(photo => photo.UserId == userId).Select(photo => photo.ToBllPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get all photos for user was failed.", this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method return photo by category.
        /// </summary>
        /// <param name="categoryPhoto">Category of photos.</param>
        /// <returns>Photos by categoryPhoto.</returns>
        public IEnumerable<PhotoEntity> GetPhotoByCategory(string categoryPhoto)
        {
            try {
                CategoryPhotoEntity category = categoryRepository.GetByName(categoryPhoto).ToBllCategoryPhoto();
                return photoRepository.GetAll().Where(photo => photo.CategoryId == category.CategoryId).Select(photo => photo.ToBllPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get photo by category was failed.", this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method GetPhotoEntity return PhotoEntity entity by id.
        /// </summary>
        /// <param name="id">Id of photo entity.</param>
        /// <returns>PhotoEntity entity by id.</returns>
        public PhotoEntity GetPhotoEntity(int id)
        {
            try {
                return photoRepository.GetById(id).ToBllPhoto();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get photo by id was failed.", this), ex);
            }

            return null;
        }

        /// <summary>
        /// Method UpdatePhoto update exists photo.
        /// </summary>
        /// <param name="photo">PhotoEntity that need update.</param>
        public void UpdatePhoto(PhotoEntity photo)
        {
            try {
                photoRepository.Update(photo.ToDalPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Update photo was failed.", this), ex);
            }
        }

        #endregion
    }
}
