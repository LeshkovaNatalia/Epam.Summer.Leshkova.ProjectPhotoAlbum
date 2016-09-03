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

        public void UpdatePhoto(PhotoEntity photo)
        {
            try {
                photoRepository.Update(photo.ToDalPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Update photo was failed.", this), ex);
            }
        }
    }
}
