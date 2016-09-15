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

        #region Public Methods

        /// <summary>
        /// Method CreateCategoryPhoto use for add new category photo.
        /// </summary>
        /// <param name="ctgrPhoto">CategoryPhotoEntity that need to add.</param>
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

        /// <summary>
        /// Method DeleteCategoryPhoto use for delete category photo.
        /// </summary>
        /// <param name="ctgrPhoto">CategoryPhotoEntity that need delete.</param>
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

        /// <summary>
        /// Method return all categories of photos.
        /// </summary>
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

        /// <summary>
        /// Method need to find if of category photo by its name.
        /// </summary>
        /// <param name="name">Name of category photo.</param>
        /// <returns>Id of category photo.</returns>
        public int GetCategoryIdByName(string name)
        {
            try { 
                return ctgrPhotoRepository.GetByPredicate(ctgr => ctgr.Name.ToUpper() == name.ToUpper()).ToBllCategoryPhoto().CategoryId;
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get category id by name {0} was failed.", name), this), ex);
            }

            return default(int);
        }

        /// <summary>
        /// Method use for get gategory photo entity by its id.
        /// </summary>
        /// <param name="id">Id of category photo.</param>
        /// <returns>Category photo entity.</returns>
        public CategoryPhotoEntity GetCategoryPhotoEntity(int id)
        {
            try {
                return ctgrPhotoRepository.GetByPredicate(ctgr => ctgr.Id == id).ToBllCategoryPhoto();
            }
            catch(Exception ex) {
                logger.Error(logger.GetMessage(string.Format("Get category photo by id {0} was failed.", id), this), ex);
            }

            return null;
        }

        #endregion
    }
}
