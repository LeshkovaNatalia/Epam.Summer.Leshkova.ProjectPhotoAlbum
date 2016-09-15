using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.ViewModels;
using MvcPL.Infrastructure.Mappers;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult UsersEdit()
        {
            var model = userService.GetAllUserEntities().Select(user => new UserViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                CreatedOn = user.CreatedOn
            });

            return View(model);
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
            {
                return HttpNotFound();
            }

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