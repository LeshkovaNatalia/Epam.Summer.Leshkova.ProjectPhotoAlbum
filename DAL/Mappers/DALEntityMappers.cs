using DAL.Interface.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DALEntityMappers
    {
        #region Extension Methods

        /// <summary>
        /// Method convert collection of roles to DalRole.
        /// </summary>
        /// <param name="roles">Collection that need convert.</param>
        /// <returns>Colecction of dal roles.</returns>
        public static ICollection<DalRole> ToDalRoles(this ICollection<Role> roles)
        {
            ICollection<DalRole> dalRoles = new HashSet<DalRole>();
            foreach (var role in roles)
                dalRoles.Add(new DalRole { Name = role.Name });

            return dalRoles;
        }

        /// <summary>
        /// Method convert role to DalRole.
        /// </summary>
        /// <param name="role">Entity that need convert.</param>
        /// <returns>Dal role.</returns>
        public static DalRole ToDalRole(this Role role)
        {
            DalRole dalRole = new DalRole { Name = role.Name };
            return dalRole;
        }

        /// <summary>
        /// Method convert collection of dal roles to roles.
        /// </summary>
        /// <param name="dalRoles">Collection that need convert.</param>
        /// <returns>Collection of roles.</returns>
        public static ICollection<Role> ToRoles(this ICollection<DalRole> dalRoles)
        {
            ICollection<Role> roles = new HashSet<Role>();
            foreach (var role in dalRoles)
                roles.Add(new Role { Name = role.Name, RoleId = role.Id });

            return roles;
        }

        #region Expression Converters

        /// <summary>
        /// Method convert predicate of type DalPhoto to type Photo.
        /// </summary>
        public static Func<Photo, bool> ConvertExpressionPhoto(this Expression<Func<DalPhoto, bool>> f)
        {
            Expression<Func<Photo, DalPhoto>> convert = ph => new DalPhoto {
                Id = ph.PhotoId,
                CategoryId = ph.CategoryId,
                CreatedOn = ph.CreatedOn,
                Description = ph.Description,
                Image = ph.Image,
                UserId = ph.UserId
            };

            var parameter = Expression.Parameter(typeof(Photo));
            var body = Expression.Invoke(f, Expression.Invoke(convert, parameter));
            var lambda = Expression.Lambda<Func<Photo, bool>>(body, parameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Method convert predicate of type DalRole to type Role.
        /// </summary>
        public static Func<Role, bool> ConvertExpressionRole(this Expression<Func<DalRole, bool>> f)
        {
            Expression<Func<Role, DalRole>> convert = role => new DalRole
            {
                Id = role.RoleId,
                Name = role.Name
            };

            var parameter = Expression.Parameter(typeof(Role));
            var body = Expression.Invoke(f, Expression.Invoke(convert, parameter));
            var lambda = Expression.Lambda<Func<Role, bool>>(body, parameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Method convert predicate of type DalVoting to type Voting.
        /// </summary>
        public static Func<Voting, bool> ConvertExpressionVote(this Expression<Func<DalVoting, bool>> f)
        {
            Expression<Func<Voting, DalVoting>> convert = vote => new DalVoting
            {
                PhotoId = vote.PhotoId,
                UserId = vote.UserId,
                Rating = vote.Rating
            };

            var parameter = Expression.Parameter(typeof(Voting));
            var body = Expression.Invoke(f, Expression.Invoke(convert, parameter));
            var lambda = Expression.Lambda<Func<Voting, bool>>(body, parameter);

            return lambda.Compile();
        }

        /// <summary>
        /// Method convert predicate of type DalUser to type User.
        /// </summary>
        public static Func<User, bool> ConvertExpressionUser(this Expression<Func<DalUser, bool>> f)
        {
            Expression<Func<User, DalUser>> convert = user => new DalUser
            {
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,
                Login = user.Email,
                CreatedOn = user.CreatedOn,
                Roles = user.Roles.ToDalRoles()
            };

            var parameter = Expression.Parameter(typeof(Voting));
            var body = Expression.Invoke(f, Expression.Invoke(convert, parameter));
            var lambda = Expression.Lambda<Func<User, bool>>(body, parameter);

            return lambda.Compile();
        }

        #endregion

        #endregion
    }
}
