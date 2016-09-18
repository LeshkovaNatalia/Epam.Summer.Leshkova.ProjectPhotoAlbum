using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Infrastructure.Attributes;
using MvcPL.Infrastructure.Utilities;
using MvcPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;

namespace MvcPL.Controllers
{
    public class CategoryController : Controller
    {
        #region Fields
        private readonly ICategoryPhotoService categoryService;
        #endregion

        #region Ctors
        public CategoryController(ICategoryPhotoService service)
        {
            categoryService = service;
        }
        #endregion

        #region Public Methods

        [Authorize]
        [HttpGet]
        public ActionResult CreateCategory()
        {
            if (Request.QueryString["content"] != null)
                return PartialView();
            else
                return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryPhotoViewModel ctgrPhotoViewModel, string returnUrl)
        {
            if (categoryService.GetCategoryIdByName(ctgrPhotoViewModel.Name) != 0)
            {
                ModelState.AddModelError("", "Category already exists.");
                if (HttpContext.Request.IsAjaxRequest())
                    return Json(new { errors = GetErrorsFromModelState() });                
                return View(ctgrPhotoViewModel);
            }

            categoryService.CreateCategoryPhoto(ctgrPhotoViewModel.ToBllCategory());
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new { success = true, redirect = returnUrl });
            return RedirectToAction("Index", "Photo");
        }

        public JsonResult ValidateCategoryName(string Name)
        {
            var anyCategory = categoryService.GetCategoryIdByName(Name);
            return Json(anyCategory == 0, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCategoryPhoto(int id = 1)
        {
            var result = categoryService.GetAllCategoryPhotoEntities().Select(ctgrPhoto => ctgrPhoto.ToMvcCtgrPhoto());
            var rslt = result.ToList().Select(c => new
            {
                CategoryName = c.Name,
                CategoryId = c.Id,
                Selected = false
            }).ToList();

            ViewBag.CategoryPhoto = new SelectList(rslt, "CategoryId", "CategoryName");

            return View();
        }

        public string GetCategory(int categoryId)
        {
            var category = categoryService.GetCategoryPhotoEntity(categoryId);

            return category.Name;
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