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

        public void Create(DalUser dalUser)
        {
            var user = new User()
            {
                Login = dalUser.Email,
                Password = dalUser.Password,
                Email = dalUser.Email,
                Photo = dalUser.Photo,
                CreatedOn = dalUser.CreatedOn,
                Roles = new List<Role>()//dalUser.Roles.ToRoles()
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

        public void Delete(DalUser dalUser)
        {
            User user = context.Set<User>().First(u => u.UserId == dalUser.Id);
            user.Roles.Remove(new Role() { Name = "User" });

            IEnumerable<DalVoting> votings = context.Set<Voting>().Where(usr => usr.UserId == dalUser.Id).Select(vote => new DalVoting()
            {
                UserId = dalUser.Id
            });

            if (votings != null)
                foreach(var vote in votings)
                    if(vote.UserId != 0)
                        context.Set<Voting>().Remove(new Voting() { UserId = vote.UserId, PhotoId = vote.PhotoId});

            IEnumerable<DalPhoto> photos = context.Set<Photo>().Where(usr => usr.UserId == dalUser.Id).Select(ph => new DalPhoto()
            {
                UserId = dalUser.Id
            });

            foreach (var photo in photos)
                if (photo.UserId != 0)
                    context.Set<Photo>().Remove(new Photo() { UserId = photo.UserId});
            
            context.Set<User>().Remove(user);            
        }

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
        
        public DalUser GetById(int key)
        {
            DalUser dalUser = context.Set<User>().Select(user => new DalUser()
            {
                Id = user.UserId,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                CreatedOn = user.CreatedOn,
                Photo = user.Photo
            }).SingleOrDefault(usr => usr.Id == key);

            User cur = context.Set<User>().Single(user => user.UserId == key);
            foreach(var r in cur.Roles)
                dalUser.Roles.Add(r.ToDalRole());

            return dalUser;
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

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

        public void Update(DalUser entity)
        {
            var actualUser = GetById(entity.Id);
            User updatedUser = new User()
            {
                UserId = entity.Id,
                Login = entity.Login,
                Email = entity.Email,
                Password = entity.Password,
                Roles = actualUser.Roles.ToRoles(),
                Photo = entity.Photo,
                CreatedOn = entity.CreatedOn                
            };

            context.Entry(updatedUser).State = EntityState.Modified;
            context.SaveChanges();
        }

        #endregion
    }
}
