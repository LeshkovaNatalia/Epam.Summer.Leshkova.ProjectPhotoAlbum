using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.ViewModels;
using MvcPL.Infrastructure.Mappers;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        #region Fields
        private readonly IUserService userService;
        #endregion

        #region Ctors
        public AdminController(IUserService service)
        {
            userService = service;
        }
        #endregion

        #region Public Methods

        [ActionName("Index")]
        public ActionResult UsersEdit(int page = 0)
        {
            int pageSize = 10;
            var users = new PagedData<UserViewModel>();
            var allusers = userService.GetAllUserEntities();
            users = GetCountOfUsers(pageSize, page, allusers);      
            
            if (HttpContext.Request.IsAjaxRequest())
                return PartialView(users);
            else
                return View(users);
        }

        private PagedData<UserViewModel> GetCountOfUsers(int pageSize, int page, IEnumerable<UserEntity> allusers)
        {
            var resultUsers = new PagedData<UserViewModel>();
            if (page > 0)
            {
                resultUsers.Data = GetUsersData(pageSize, page);
                resultUsers.CurrentPage = page;
            }
            else
            {
                resultUsers.Data = GetFirstPageUsersData(pageSize, page); 
                resultUsers.CurrentPage = 1;
            }

            resultUsers.NumberOfPages = allusers.Count() % pageSize == 0 ? allusers.Count() / pageSize : allusers.Count() / pageSize + 1;
            return resultUsers;
        }

        private IEnumerable<UserViewModel> GetFirstPageUsersData(int pageSize, int page)
        {
            var users = userService.GetAllUserEntities().Select(user => new UserViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                CreatedOn = user.CreatedOn
            }).Take(pageSize)
                        .ToList();
            return users;
        }

        private IEnumerable<UserViewModel> GetUsersData(int pageSize, int page)
        {
            var users = userService.GetAllUserEntities().Select(user => new UserViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                CreatedOn = user.CreatedOn
            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
            return users;
        }

        public ActionResult Details(int userId)
        {
            var user = userService.GetUserEntity(userId).ToMvcUser();
            return View(user);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            UserEntity user = userService.GetUserEntity(id);
            userService.DeleteUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserEntity user = userService.GetUserEntity(id);
            if (user == null)
                return HttpNotFound();

            return View(user.ToMvcUser());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserEntity user = userService.GetUserEntity(id);
            userService.DeleteUser(user);
            return RedirectToAction("Index");
        }

        #endregion
    }
}