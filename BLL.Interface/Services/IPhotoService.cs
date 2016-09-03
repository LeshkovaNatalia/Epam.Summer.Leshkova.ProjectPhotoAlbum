using BLL.Interface.Entities;
using System.Collections.Generic;

namespace BLL.Interface.Services
{
    public interface IPhotoService
    {
        PhotoEntity GetPhotoEntity(int id);
        IEnumerable<PhotoEntity> GetAllPhotoEntities();
        IEnumerable<PhotoEntity> GetAllPhotoEntitiesForUser(int userId);
        void CreatePhoto(PhotoEntity photo);
        void DeletePhoto(PhotoEntity photo);
        void UpdatePhoto(PhotoEntity photo);
        IEnumerable<PhotoEntity> GetPhotoByCategory(string categoryPhoto);
    }
}
