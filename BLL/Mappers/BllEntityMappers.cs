using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Collections.Generic;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
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

        public static ICollection<DalRole> ToDalRoles(this ICollection<RoleEntity> roles)
        {
            ICollection<DalRole> dalRoles = new HashSet<DalRole>();
            foreach (var role in roles)
                dalRoles.Add(new DalRole { Name = role.Name });

            return dalRoles;
        }

        public static CategoryPhotoEntity ToBllCategoryPhoto(this DalCategoryPhoto ctgrPhoto)
        {
            return new CategoryPhotoEntity()
            {
                CategoryId = ctgrPhoto.Id,
                Name = ctgrPhoto.Name
            };
        }

        public static DalCategoryPhoto ToDalCategoryPhoto(this CategoryPhotoEntity ctgrPhoto)
        {
            return new DalCategoryPhoto()
            {
                Id = ctgrPhoto.CategoryId,
                Name = ctgrPhoto.Name
            };
        }

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

        public static ICollection<RoleEntity> ToDalRole(this ICollection<DalRole> dalRoles)
        {
            ICollection<RoleEntity> entityRoles = new HashSet<RoleEntity>();
            foreach (var role in dalRoles)
                entityRoles.Add(new RoleEntity { Name = role.Name });

            return entityRoles;
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                RoleId = dalRole.Id,
                Name = dalRole.Name
            };
        }

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

        public static VotingEntity ToBllVoting(this DalVoting dalVoting)
        {
            return new VotingEntity()
            {
                UserId = dalVoting.UserId,
                PhotoId = dalVoting.PhotoId,
                Rating = dalVoting.Rating
            };
        }

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

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.RoleId,
                Name = roleEntity.Name
            };
        }

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
}
