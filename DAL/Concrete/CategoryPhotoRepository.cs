using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;
using ORM;
using DAL.Concrete;

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

        public void Create(DalCategoryPhoto entity)
        {
            var categoryPhoto = new CategoryPhoto()
            {
                Name = entity.Name
            };

            context.Set<CategoryPhoto>().Add(categoryPhoto);
        }

        public void Delete(DalCategoryPhoto entity)
        {
            var categoryPhoto = new CategoryPhoto()
            {
                Name = entity.Name
            };

            context.Set<CategoryPhoto>().Remove(categoryPhoto);
        }

        public IEnumerable<DalCategoryPhoto> GetAll()
        {
            return context.Set<CategoryPhoto>().Select(ctgrPhoto => new DalCategoryPhoto()
            {
                Id = ctgrPhoto.CategoryId,
                Name = ctgrPhoto.Name,
            });
        }

        public DalCategoryPhoto GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalCategoryPhoto GetByPredicate(Expression<Func<DalCategoryPhoto, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalCategoryPhoto entity)
        {
            throw new NotImplementedException();
        }

        public DalCategoryPhoto GetByName(string nameCategory)
        {
            var category = context.Set<CategoryPhoto>().FirstOrDefault(c => c.Name == nameCategory);

            return new DalCategoryPhoto()
            {
                Id = category.CategoryId,
                Name = category.Name
            };
        }
    }
}
