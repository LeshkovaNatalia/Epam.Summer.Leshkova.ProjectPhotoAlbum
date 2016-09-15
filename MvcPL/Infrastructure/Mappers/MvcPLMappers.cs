using BLL.Interface.Entities;
using MvcPL.ViewModels;
using MvcPL.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPLMappers
    {
        #region Public Methods

        public static CategoryPhotoEntity ToBllCategory(this CategoryPhotoViewModel categoryViewModel)
        {
            return new CategoryPhotoEntity()
            {
                CategoryId = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }

        public static CategoryPhotoViewModel ToMvcCtgrPhoto(this CategoryPhotoEntity ctgrPhotoEntity)
        {
            return new CategoryPhotoViewModel()
            {
                Id = ctgrPhotoEntity.CategoryId,
                Name = ctgrPhotoEntity.Name
            };
        }

        public static PhotoViewModel ToMvcPhoto(this PhotoEntity photoEntity)
        {
            return new PhotoViewModel()
            {
                Id = photoEntity.Id,
                Description = photoEntity.Description,
                Image = photoEntity.Image,
                CreatedOn = photoEntity.CreatedOn,
                CategoryId = photoEntity.CategoryId,
                UserId = photoEntity.UserId,
            };
        }

        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                UserId = userEntity.UserId,
                Email = userEntity.Email,
                Photo = userEntity.Photo,
                CreatedOn = userEntity.CreatedOn,
                Roles = userEntity.Roles.ToStringRoles()
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                UserId = userViewModel.UserId,
                Email = userViewModel.Email,
                CreatedOn = userViewModel.CreatedOn,
                Photo = userViewModel.Photo,
                Password = userViewModel.Password,
                Roles = userViewModel.Roles.ToEntityRoles()
            };
        }

        public static PhotoEntity ToBllPhoto(this PhotoViewModel photoViewModel)
        {
            return new PhotoEntity()
            {
                Id = photoViewModel.Id,
                UserId = photoViewModel.UserId,
                CategoryId = photoViewModel.CategoryId,
                Image = photoViewModel.Image,
                Description = photoViewModel.Description,
                CreatedOn = photoViewModel.CreatedOn

            };
        }

        public static VotingEntity ToBllVoting(this VotingViewModel votingViewModel)
        {
            return new VotingEntity()
            {
                UserId = votingViewModel.UserId,
                PhotoId = votingViewModel.PhotoId,
                Rating = votingViewModel.Rating
            };
        }

        #endregion

        #region Private Methods

        public static string[] ToStringRoles(this ICollection<RoleEntity> rolesEntity)
        {
            var roles = new string[rolesEntity.Count];

            int i = 0;
            foreach (var role in rolesEntity)
            {
                roles[i] = role.Name;
                i++;
            }

            return roles;
        }

        public static List<RoleEntity> ToEntityRoles(this string[] rolesStrings)
        {
            var roles = new List<RoleEntity>();

            for (int i = 0; i < rolesStrings.Length; i++)
            {
                roles.Add(new RoleEntity() { Name = rolesStrings[i] });
            }
            return roles;
        }

        #endregion
    }
}