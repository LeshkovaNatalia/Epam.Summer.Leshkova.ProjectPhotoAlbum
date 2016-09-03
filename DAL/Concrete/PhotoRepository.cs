using DAL.Interface.DTO;
using DAL.Interface.Repository;
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

        public void Create(DalPhoto entity)
        {
            var photo = new Photo()
            {
                Description = entity.Description,
                Image = entity.Image,
                CreatedOn = entity.CreatedOn,
                CategoryId = entity.CategoryId,
                UserId = entity.UserId
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

        public DalPhoto GetById(int id)
        {
            return GetAll().SingleOrDefault(photo => photo.Id == id);
        }

        public DalPhoto GetByPredicate(Expression<Func<DalPhoto, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalPhoto entity)
        {
            var actualPhoto = GetById(entity.Id);
            Photo updatedPhoto = new Photo()
            {
                PhotoId = actualPhoto.Id,
                Image = actualPhoto.Image,
                CategoryId = entity.CategoryId,
                CreatedOn = actualPhoto.CreatedOn,
                Description = entity.Description,
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
