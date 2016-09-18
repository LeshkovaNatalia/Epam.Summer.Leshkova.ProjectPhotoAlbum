using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using System.Data.Entity;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly DbContext context;
        #endregion

        #region Ctors
        public UserRepository(DbContext dbContext)
        {
            this.context = dbContext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method Create create user entity.
        /// </summary>
        /// <param name="dalUser">New user entity for create.</param>
        public void Create(DalUser dalUser)
        {
            var user = new User()
            {
                Login = dalUser.Email,
                Password = dalUser.Password,
                Email = dalUser.Email,
                Photo = dalUser.Photo,
                CreatedOn = dalUser.CreatedOn,
                Roles = new List<Role>()
            };

            Role myRole = context.Set<Role>().FirstOrDefault(r => r.RoleId == 3);
            user.Roles.Add(myRole);

            context.Set<User>().Add(user);
        }

        public void Create(DalUser dalUser, DalRole dalRole)
        {
            var user = new User()
            {
                Login = dalUser.Email,
                Password = dalUser.Password,
                Email = dalUser.Email,
                Photo = dalUser.Photo, 
                CreatedOn = dalUser.CreatedOn
            };

            var role = new Role()
            {
                Name = dalRole.Name
            };

            user.Roles.Add(role);
            context.Set<User>().Add(user);
        }

        /// <summary>
        /// Method Delete user.
        /// </summary>
        /// <param name="dalUser">Entity that need delete.</param>
        public void Delete(DalUser dalUser)
        {
            User user = context.Set<User>().First(u => u.UserId == dalUser.Id);
            user.Roles.Remove(new Role() { Name = "User" });

            RemoveVotings(context, dalUser.Id);
            RemovePhotos(context, dalUser.Id);
            
            context.Set<User>().Remove(user);            
        }

        private void RemovePhotos(DbContext context, int id)
        {
            IEnumerable<DalPhoto> photos = context.Set<Photo>().Where(usr => usr.UserId == id).Select(ph => new DalPhoto()
            {
                UserId = id,
                Id = ph.PhotoId,
                CategoryId = ph.CategoryId,
                CreatedOn = ph.CreatedOn,
                Image = ph.Image,
                Description = ph.Description
            });

            if (photos.Count() != 0)
                foreach (var photo in photos)
                    if (photo.UserId != 0)
                    {
                        var ph = new Photo()
                        {
                            UserId = photo.UserId,
                            PhotoId = photo.Id,
                            CreatedOn = photo.CreatedOn,
                            CategoryId = photo.CategoryId,
                            Image = photo.Image,
                            Description = photo.Description
                        };
                        RemoveVotings(context, photo.UserId, photo.Id);
                        context.Set<Photo>().Attach(ph);
                        context.Set<Photo>().Remove(ph);
                    }
        }

        private void RemoveVotings(DbContext context, int id, int photoId = 0)
        {
            IEnumerable<DalVoting> votings;
            if(photoId == 0)
                votings = context.Set<Voting>().Where(usr => usr.UserId == id).Select(vote => new DalVoting()
            {
                UserId = id,
                PhotoId = vote.PhotoId,
                Rating = vote.Rating
            });
            else
                votings = context.Set<Voting>().Where(ph => ph.PhotoId == photoId).Select(vote => new DalVoting()
                {
                    UserId = vote.UserId,
                    PhotoId = photoId,
                    Rating = vote.Rating
                });

            if (votings != null)
                foreach (var vote in votings)
                {
                    var entity = new Voting()
                    {
                        UserId = vote.UserId,
                        PhotoId = vote.PhotoId,
                        Rating = vote.Rating
                    };
                    context.Set<Voting>().Attach(entity);
                    context.Set<Voting>().Remove(entity);
                }
        }

        /// <summary>
        /// Method GetAll return all users.
        /// </summary>
        /// <returns>Lists of users entity.</returns>
        public IEnumerable<DalUser> GetAll()
        {
            IEnumerable<DalUser> users = context.Set<User>().Select(user => new DalUser()
            {
                Id = user.UserId,
                Login = user.Login,
                Password = user.Password,
                CreatedOn = user.CreatedOn,
                Email = user.Email,
                Photo = user.Photo
            });

            List<DalUser> dalUsers = new List<DalUser>();

            foreach (var u in users)
            {
                User cur = context.Set<User>().Single(user => user.Email == u.Email);
                foreach (var r in cur.Roles.ToDalRoles())
                {
                    u.Roles.Add(r);                   
                }
                dalUsers.Add(new DalUser() { Id = cur.UserId, Login = cur.Login, Password = cur.Password, CreatedOn = cur.CreatedOn, Email = cur.Email, Photo = cur.Photo, Roles = u.Roles });
            }

            users = dalUsers.AsEnumerable();       

            return users;
        }

        /// <summary>
        /// Method GetById return DalUser entity by id.
        /// </summary>
        /// <param name="id">Id of user entity.</param>
        /// <returns>DalUser entity by id.</returns>
        public DalUser GetById(int id)
        {
            DalUser dalUser = context.Set<User>().Select(user => new DalUser()
            {
                Id = user.UserId,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                CreatedOn = user.CreatedOn,
                Photo = user.Photo
            }).SingleOrDefault(usr => usr.Id == id);

            User cur = context.Set<User>().Single(user => user.UserId == id);
            foreach(var r in cur.Roles)
                dalUser.Roles.Add(r.ToDalRole());

            return dalUser;
        }

        /// <summary>
        /// Method GetByPredicate return DalUser by predicate.
        /// </summary>
        /// <param name="f">Predicate.</param>
        /// <returns>Return DalUser by predicate f.</returns>
        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            User item = context.Set<User>().SingleOrDefault(f.ConvertExpressionUser());

            return new DalUser
            {
                Id = item.UserId,                
                Login = item.Login,
                Email = item.Email,
                Password = item.Password,
                CreatedOn = item.CreatedOn,
                Photo = item.Photo,
                Roles = item.Roles.ToDalRoles()
            };
        }

        /// <summary>
        /// Method return user entity by email.
        /// </summary>
        /// <param name="email">User's email.</param>
        /// <returns>User entity by email.</returns>
        public DalUser GetUserByEmail(string email)
        {
            DalUser dalUser = context.Set<User>().Select(user => new DalUser()
            {
                Id = user.UserId,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                CreatedOn = user.CreatedOn,
                Photo = user.Photo
            }).SingleOrDefault(usr => usr.Email == email);

            if (dalUser != null)
            {
                User cur = context.Set<User>().Single(user => user.Email == dalUser.Email);
                foreach(var r in cur.Roles)
                    dalUser.Roles.Add(r.ToDalRole());
                return dalUser;
            }

            return null;
        }

        /// <summary>
        /// Method Update update exists user.
        /// </summary>
        /// <param name="dalUser">DalUser that need update.</param>
        public void Update(DalUser dalUser)
        {
            var actualUser = GetById(dalUser.Id);
            User updatedUser = new User()
            {
                UserId = dalUser.Id,
                Login = dalUser.Login,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Roles = actualUser.Roles.ToRoles(),
                Photo = dalUser.Photo,
                CreatedOn = dalUser.CreatedOn                
            };

            context.Entry(updatedUser).State = EntityState.Modified;
            context.SaveChanges();
        }

        #endregion
    }
}
