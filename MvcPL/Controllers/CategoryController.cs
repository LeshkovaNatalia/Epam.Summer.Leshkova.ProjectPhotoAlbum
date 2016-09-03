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
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryPhotoViewModel ctgrPhotoViewModel)
        {
            categoryService.CreateCategoryPhoto(ctgrPhotoViewModel.ToBllCategory());
            return RedirectToAction("Index", "Photo");
        }

        public ActionResult GetAllCategoryPhoto(int id = 1)
        {
            var result = categoryService.GetAllCategoryPhotoEntities().Select(ctgrPhoto => ctgrPhoto.ToMvcCtgrPhoto());
            var rslt = result.ToList().Select(c => new
            {
                CategoryName = c.Name,
                CategoryId = c.Id,
                Selected = (c.Id == id)
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
    }
}