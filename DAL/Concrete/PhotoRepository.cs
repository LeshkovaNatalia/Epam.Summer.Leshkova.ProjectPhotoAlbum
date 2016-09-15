using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;
using ORM;

namespace DAL.Concrete
{
    public class PhotoRepository : IRepository<DalPhoto>
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Ctors
        public PhotoRepository(DbContext uow)
        {
            this.context = uow;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method Create create photo entity.
        /// </summary>
        /// <param name="dalPhoto">New photo entity for create.</param>
        public void Create(DalPhoto dalPhoto)
        {
            var photo = new Photo()
            {
                Description = dalPhoto.Description,
                Image = dalPhoto.Image,
                CreatedOn = dalPhoto.CreatedOn,
                CategoryId = dalPhoto.CategoryId,
                UserId = dalPhoto.UserId
            };

            context.Set<Photo>().Add(photo);
        }

        /// <summary>
        /// Method Delete at first delete all votings with the same id in dalPhoto. 
        /// Then delete photo.
        /// </summary>
        /// <param name="dalPhoto">Entity that need delete.</param>
        public void Delete(DalPhoto dalPhoto)
        {
            var votings = context.Set<Voting>().Where(p => p.PhotoId == dalPhoto.Id);

            if (votings.Count() != 0)
                foreach (var vote in votings)
                    if (vote.UserId != 0)
                    {
                        Voting vt = context.Set<Voting>().Single(v => v.PhotoId == vote.PhotoId && v.UserId == vote.UserId);
                        context.Set<Voting>().Remove(vt);
                    }
            Photo photo = context.Set<Photo>().Single(p => p.PhotoId == dalPhoto.Id);
            context.Set<Photo>().Remove(photo);
        }

        /// <summary>
        /// Method GetAll return all photos.
        /// </summary>
        /// <returns>Lists of photos entity.</returns>
        public IEnumerable<DalPhoto> GetAll()
        {
            return context.Set<Photo>().Select(photo => new DalPhoto()
            {
                Id = photo.PhotoId,
                Description = photo.Description,
                Image = photo.Image,
                CreatedOn = photo.CreatedOn,
                CategoryId = photo.CategoryId,
                UserId = photo.UserId
            });
        }

        /// <summary>
        /// Method GetById return DalPhoto entity by id.
        /// </summary>
        /// <param name="id">Id of photo entity.</param>
        /// <returns>DalPhoto entity by id.</returns>
        public DalPhoto GetById(int id)
        {
            return GetAll().SingleOrDefault(photo => photo.Id == id);
        }

        /// <summary>
        /// Method GetByPredicate return DalPhoto by predicate.
        /// </summary>
        /// <param name="f">Predicate.</param>
        /// <returns>Return DalPhoto by predicate f.</returns>
        public DalPhoto GetByPredicate(Expression<Func<DalPhoto, bool>> f)
        {
            Photo item = context.Set<Photo>().SingleOrDefault(f.ConvertExpressionPhoto());

            return new DalPhoto {
                Id = item.PhotoId,
                CategoryId = item.CategoryId,
                CreatedOn = item.CreatedOn,
                Description = item.Description,
                UserId = item.UserId,
                Image = item.Image
            };
        }

        /// <summary>
        /// Method Update update exists photo.
        /// </summary>
        /// <param name="dalPhoto">DalPhoto that need update.</param>
        public void Update(DalPhoto dalPhoto)
        {
            var actualPhoto = GetById(dalPhoto.Id);
            Photo updatedPhoto = new Photo()
            {
                PhotoId = actualPhoto.Id,
                Image = actualPhoto.Image,
                CategoryId = dalPhoto.CategoryId,
                CreatedOn = actualPhoto.CreatedOn,
                Description = dalPhoto.Description,
                UserId = actualPhoto.UserId
            };
            context.Set<Photo>().Attach(updatedPhoto);
            var photo = context.Entry(updatedPhoto);
            photo.Property(ph => ph.Description).IsModified = true;
            photo.Property(ph => ph.CategoryId).IsModified = true;
            context.SaveChanges();
        }

        #endregion
    }
}
