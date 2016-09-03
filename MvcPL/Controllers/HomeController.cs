using BLL.Interface.Services;
using MvcPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly IUserService userService;
        #endregion

        #region Ctors
        public HomeController(IUserService service)
        {
            userService = service;
        }
        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            return RedirectToAction("GetAllPhoto", "Photo");
        }

        public ActionResult About()
        {
            return View();
        }       

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public Image GetPhoto(string email)
        {
            var user = userService.GetUserByEmail(email);
            
            if ((user.Photo) != null)
                return GetImageFromBytes(user.Photo);
            else
                return null;
        }

        #endregion

        #region Private Methods
        private Image GetImageFromBytes(byte[] photo)
        {
            MemoryStream m = new MemoryStream(photo);
            Image img = System.Drawing.Image.FromStream(m);
            img.Save(Response.OutputStream, ImageFormat.Png);

            return img;
        }

        #endregion
    }
}