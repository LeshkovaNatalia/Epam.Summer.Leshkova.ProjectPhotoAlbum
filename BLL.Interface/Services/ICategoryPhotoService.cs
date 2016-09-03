using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface ICategoryPhotoService
    {
        CategoryPhotoEntity GetCategoryPhotoEntity(int id);
        IEnumerable<CategoryPhotoEntity> GetAllCategoryPhotoEntities();
        void CreateCategoryPhoto(CategoryPhotoEntity ctgrPhoto);
        void DeleteCategoryPhoto(CategoryPhotoEntity ctgrPhoto);
        int GetCategoryIdByName(string name);
    }
}
