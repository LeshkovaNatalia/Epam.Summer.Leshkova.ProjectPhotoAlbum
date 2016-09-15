using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class CategoryPhotoRepository : ICategoryPhotoRepository
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Ctors
        public CategoryPhotoRepository(DbContext uow)
        {
            this.context = uow;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method Create create category entity.
        /// </summary>
        /// <param name="dalCategory">New category entity for create.</param>
        public void Create(DalCategoryPhoto dalCategory)
        {
            var categoryPhoto = new CategoryPhoto()
            {
                Name = dalCategory.Name
            };

            context.Set<CategoryPhoto>().Add(categoryPhoto);
        }

        /// <summary>
        /// Method Delete dalPhoto. 
        /// </summary>
        /// <param name="dalCategory">Entity that need delete.</param>
        public void Delete(DalCategoryPhoto dalCategory)
        {
            var categoryPhoto = new CategoryPhoto()
            {
                Name = dalCategory.Name
            };

            context.Set<CategoryPhoto>().Remove(categoryPhoto);
        }

        /// <summary>
        /// Method GetAll return all categories.
        /// </summary>
        /// <returns>Lists of categories entity.</returns>
        public IEnumerable<DalCategoryPhoto> GetAll()
        {
            return context.Set<CategoryPhoto>().Select(ctgrPhoto => new DalCategoryPhoto()
            {
                Id = ctgrPhoto.Id,
                Name = ctgrPhoto.Name,
            });
        }

        /// <summary>
        /// Method GetById return DalCategoryPhoto entity by id.
        /// </summary>
        /// <param name="id">Id of category entity.</param>
        /// <returns>DalCategoryPhoto entity by id.</returns>
        public DalCategoryPhoto GetById(int id)
        {
            return GetAll().SingleOrDefault(categoryPhoto => categoryPhoto.Id == id);
        }

        /// <summary>
        /// Method GetByPredicate return DalCategoryPhoto by predicate.
        /// </summary>
        /// <param name="f">Predicate.</param>
        /// <returns>Return DalCategoryPhoto by predicate f.</returns>
        public DalCategoryPhoto GetByPredicate(Expression<Func<DalCategoryPhoto, bool>> f)
        {       
            var convert = ExpressionConverter.Convert<DalCategoryPhoto, CategoryPhoto>(f);

            CategoryPhoto item = context.Set<CategoryPhoto>().SingleOrDefault(convert);
            
            return new DalCategoryPhoto { Name = item.Name, Id = item.Id};
        }

        /// <summary>
        /// Method Update update exists category.
        /// </summary>
        /// <param name="dalCategory">DalCategoryPhoto that need update.</param>
        public void Update(DalCategoryPhoto dalCategory)
        {
            var actualCategory = GetById(dalCategory.Id);
            CategoryPhoto updatedCategory = new CategoryPhoto()
            {
                Id = actualCategory.Id,
                Name = actualCategory.Name
            };
            context.Set<CategoryPhoto>().Attach(updatedCategory);
            var category = context.Entry(updatedCategory);
            category.Property(ctg => ctg.Name).IsModified = true;
            context.SaveChanges();
        }

        /// <summary>
        /// Method GetByName return category by name.
        /// </summary>
        /// <param name="nameCategory">Name of category.</param>
        /// <returns>Return DalCategoryPhoto by name.</returns>
        public DalCategoryPhoto GetByName(string nameCategory)
        {
            var category = context.Set<CategoryPhoto>().FirstOrDefault(c => c.Name == nameCategory);

            return new DalCategoryPhoto()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
            
        #endregion
    }
}
