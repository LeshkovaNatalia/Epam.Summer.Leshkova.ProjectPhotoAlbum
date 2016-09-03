using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using Logger;

namespace BLL.Services
{
    public class CategoryPhotoService : ICategoryPhotoService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly ICategoryPhotoRepository ctgrPhotoRepository;
        private readonly ILogger logger;
        #endregion

        #region Ctors

        public CategoryPhotoService(IUnitOfWork uow, ICategoryPhotoRepository repository)
        {
            this.uow = uow;
            this.ctgrPhotoRepository = repository;
            logger = GlobalLogger.Logger;
        }

        #endregion

        public void CreateCategoryPhoto(CategoryPhotoEntity ctgrPhoto)
        {
            try {
                ctgrPhotoRepository.Create(ctgrPhoto.ToDalCategoryPhoto());
                uow.Commit();
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Create category photo was failed.", this), ex);
            }
        }

        public void DeleteCategoryPhoto(CategoryPhotoEntity ctgrPhoto)
        {
            try {
                ctgrPhotoRepository.Delete(ctgrPhoto.ToDalCategoryPhoto());
                uow.Commit();
            }
            catch (Exception ex) {
                logger.Error(logger.GetMessage("Delete category photo was failed.", this), ex);
            }
        }

        public IEnumerable<CategoryPhotoEntity> GetAllCategoryPhotoEntities()
        {
            try {
                return ctgrPhotoRepository.GetAll().Select(ctgrPhoto => ctgrPhoto.ToBllCategoryPhoto());
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage("Get all category photo was failed.", this), ex);
            }

            return null;
        }

        public int GetCategoryIdByName(string name)
        {
            try
            {
                return ctgrPhotoRepository.GetAll().FirstOrDefault(cat => cat.Name == name).Id;
            }
            catch(Exception ex)
            {
                logger.Error(logger.GetMessage("Get category id by name was failed.", this), ex);
            }

            return default(int);
        }

        public CategoryPhotoEntity GetCategoryPhotoEntity(int id)
        {
            try {
                return ctgrPhotoRepository.GetAll().FirstOrDefault(cat => cat.Id == id).ToBllCategoryPhoto();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get category photo by id {0} was failed.", id), this), ex);
            }

            return null;
        }
    }
}
