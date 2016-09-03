﻿using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;
using DAL.Interface.DTO;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<PhotoAlbum>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<PhotoAlbum>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();

            kernel.Bind<ICategoryPhotoService>().To<CategoryPhotoService>();
            kernel.Bind<ICategoryPhotoRepository>().To<CategoryPhotoRepository>();

            kernel.Bind<IPhotoService>().To<PhotoService>();
            kernel.Bind<IRepository<DalPhoto>>().To<PhotoRepository>();

            kernel.Bind<IVotingService>().To<VotingService>();
            kernel.Bind<IVotingRepository>().To<VotingRepository>();
        }
    }
}