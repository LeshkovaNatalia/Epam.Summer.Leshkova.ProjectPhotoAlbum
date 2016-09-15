using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Collections.Generic;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region Extension Methods

        #region for User Entity

        /// <summary>
        /// Method ToDalUser convert UserEntity to DalUser.
        /// </summary>
        /// <param name="userEntity">UserEntity that need convert.</param>
        /// <returns>DalUser entity.</returns>
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.UserId,
                Login = userEntity.Email,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Photo = userEntity.Photo,
                Roles = userEntity.Roles.ToDalRoles(),
                CreatedOn = userEntity.CreatedOn
            };
        }

        /// <summary>
        /// Method ToBllUser convert DalUser to UserEntity.
        /// </summary>
        /// <param name="dalUser">DalUser that need convert.</param>
        /// <returns>UserEntity entity.</returns>
        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser != null)
                return new UserEntity()
                {
                    UserId = dalUser.Id,
                    Password = dalUser.Password,
                    Login = dalUser.Email,
                    Email = dalUser.Email,
                    Photo = dalUser.Photo,
                    CreatedOn = dalUser.CreatedOn,
                    Roles = dalUser.Roles.ToDalRole()
                };

            return null;
        }

        #endregion

        #region for Role entity

        /// <summary>
        /// Method ToDalRoles convert collection of RoleEntity to collection of DalRole.
        /// </summary>
        /// <param name="roles">Collection of RoleEntity that need convert.</param>
        /// <returns>Collection of DalRole entities.</returns>
        public static ICollection<DalRole> ToDalRoles(this ICollection<RoleEntity> roles)
        {
            ICollection<DalRole> dalRoles = new HashSet<DalRole>();
            foreach (var role in roles)
                dalRoles.Add(new DalRole { Name = role.Name });

            return dalRoles;
        }

        /// <summary>
        /// Method ToDalRole convert collection of DalRole to collection of RoleEntity.
        /// </summary>
        /// <param name="dalRoles">Collection of DalRole that need convert.</param>
        /// <returns>Collection of RoleEntity entities.</returns>
        public static ICollection<RoleEntity> ToDalRole(this ICollection<DalRole> dalRoles)
        {
            ICollection<RoleEntity> entityRoles = new HashSet<RoleEntity>();
            foreach (var role in dalRoles)
                entityRoles.Add(new RoleEntity { Name = role.Name });

            return entityRoles;
        }

        /// <summary>
        /// Method ToBllRole convert DalRole to RoleEntity.
        /// </summary>
        /// <param name="dalRole">DalRole that need convert.</param>
        /// <returns>RoleEntity entity.</returns>
        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                RoleId = dalRole.Id,
                Name = dalRole.Name
            };
        }

        /// <summary>
        /// Method ToDalRole convert RoleEntity to DalRole.
        /// </summary>
        /// <param name="roleEntity">RoleEntity that need convert.</param>
        /// <returns>DalRole entity.</returns>
        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.RoleId,
                Name = roleEntity.Name
            };
        }

        #endregion

        #region for Category entity

        /// <summary>
        /// Method ToBllCategoryPhoto convert DalCategoryPhoto to CategoryPhotoEntity.
        /// </summary>
        /// <param name="ctgrPhoto">CategoryPhotoEntity that need convert.</param>
        /// <returns>CategoryPhotoEntity entity.</returns>
        public static CategoryPhotoEntity ToBllCategoryPhoto(this DalCategoryPhoto ctgrPhoto)
        {
            return new CategoryPhotoEntity()
            {
                CategoryId = ctgrPhoto.Id,
                Name = ctgrPhoto.Name
            };
        }

        /// <summary>
        /// Method ToDalCategoryPhoto convert CategoryPhotoEntity to DalCategoryPhoto.
        /// </summary>
        /// <param name="ctgrPhoto">CategoryPhotoEntity that need convert.</param>
        /// <returns>DalCategoryPhoto entity.</returns>
        public static DalCategoryPhoto ToDalCategoryPhoto(this CategoryPhotoEntity ctgrPhoto)
        {
            return new DalCategoryPhoto()
            {
                Id = ctgrPhoto.CategoryId,
                Name = ctgrPhoto.Name
            };
        }

        #endregion

        #region for Photo entity

        /// <summary>
        /// Method ToBllPhoto convert DalPhoto to PhotoEntity.
        /// </summary>
        /// <param name="dalPhoto">DalPhoto that need convert.</param>
        /// <returns>PhotoEntity entity.</returns>
        public static PhotoEntity ToBllPhoto(this DalPhoto dalPhoto)
        {
            return new PhotoEntity()
            {
                Id = dalPhoto.Id,
                Description = dalPhoto.Description,
                Image = dalPhoto.Image,
                CreatedOn = dalPhoto.CreatedOn,
                CategoryId = dalPhoto.CategoryId,
                UserId = dalPhoto.UserId
            };
        }

        /// <summary>
        /// Method ToDalPhoto convert PhotoEntity to DalPhoto.
        /// </summary>
        /// <param name="photoEntity">PhotoEntity that need convert.</param>
        /// <returns>DalPhoto entity.</returns>
        public static DalPhoto ToDalPhoto(this PhotoEntity photoEntity)
        {
            return new DalPhoto()
            {
                Id = photoEntity.Id,
                Description = photoEntity.Description,
                Image = photoEntity.Image,
                CreatedOn = photoEntity.CreatedOn,
                CategoryId = photoEntity.CategoryId,
                UserId = photoEntity.UserId
            };
        }

        #endregion

        #region for Voting entity

        /// <summary>
        /// Method ToBllVoting convert DalVoting to VotingEntity.
        /// </summary>
        /// <param name="dalVoting">DalVoting that need convert.</param>
        /// <returns>VotingEntity entity.</returns>
        public static VotingEntity ToBllVoting(this DalVoting dalVoting)
        {
            return new VotingEntity()
            {
                UserId = dalVoting.UserId,
                PhotoId = dalVoting.PhotoId,
                Rating = dalVoting.Rating
            };
        }

        /// <summary>
        /// Method ToDalVoting convert VotingEntity to DalVoting.
        /// </summary>
        /// <param name="votingEntity">VotingEntity that need convert.</param>
        /// <returns>DalVoting entity.</returns>
        public static DalVoting ToDalVoting(this VotingEntity votingEntity)
        {
            return new DalVoting()
            {
                UserId = votingEntity.UserId,
                PhotoId = votingEntity.PhotoId,
                Rating = votingEntity.Rating
            };
        }
    }

    #endregion

    #endregion
}
