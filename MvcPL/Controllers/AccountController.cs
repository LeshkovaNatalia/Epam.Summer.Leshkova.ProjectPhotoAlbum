﻿using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Infrastructure.Utilities;
using MvcPL.Providers;
using MvcPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Fields
        private readonly IUserService userService;
        #endregion

        #region Ctors
        public AccountController(IUserService service)
        {
            userService = service;
        }
        #endregion

        #region Public Methods

        [HttpGet]
        public ActionResult LogOn(string returnUrl)
        {
            if (Request.QueryString["content"] != null)
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnViewModel viewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (HttpContext.Request.IsAjaxRequest())
                        return Json(new { success = true, redirect = returnUrl });
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Photo");
                }
                else
                    ModelState.AddModelError("", "Incorrect login or password.");
            }

            if (HttpContext.Request.IsAjaxRequest())
                return Json(new { errors = GetErrorsFromModelState() });

            return RedirectToAction("Register", "Account");
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Photo");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Request.QueryString["content"] != null)
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel, HttpPostedFileBase photo_path, string returnUrl)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { errors = GetErrorsFromModelState() });
                return View(viewModel);
            }

            if (photo_path != null && photo_path.ContentLength > 0)
            {
                using (var img = Image.FromStream(photo_path.InputStream))
                {
                    var ms = new MemoryStream();
                    img.Save(ms, img.RawFormat);
                    viewModel.AvatarImg = ms.ToArray();
                }
            }
            else
            {                
                using (var img = Image.FromFile(Server.MapPath(@"~\Content/images/default-user.png")))
                {
                    var ms = new MemoryStream();
                    img.Save(ms, img.RawFormat);
                    viewModel.AvatarImg = ms.ToArray();
                }

            }

            var anyUser = userService.GetAllUserEntities().Any(u => u.Email.Contains(viewModel.Email));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { errors = GetErrorsFromModelState() });
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Password, viewModel.AvatarImg);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    if (HttpContext.Request.IsAjaxRequest())
                        return Json(new { success = true, redirect = returnUrl });
                    return RedirectToAction("Index", "Photo");
                }
                else
                    ModelState.AddModelError("", "Error registration.");
            }
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new { errors = GetErrorsFromModelState() });
            return View(viewModel);
        }

        public JsonResult ValidateUserEmail(string Email)
        {
            var anyUser = userService.GetUserByEmail(Email);
            return Json(anyUser == null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        public string GetUserName(int userId)
        {
            var user = userService.GetUserEntity(userId);
            return user.Email;
        }
        public string GetRolesForUser(int userId)
        {
            return userService.GetRolesForUser(userId); ;
        }

        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        #endregion

        #region Private Methods
        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }
        #endregion
    }
}