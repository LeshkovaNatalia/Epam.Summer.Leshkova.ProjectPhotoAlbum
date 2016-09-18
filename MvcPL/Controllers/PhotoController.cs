using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Infrastructure.Attributes;
using MvcPL.Infrastructure.Utilities;
using MvcPL.ViewModels;
using System.IO;
using System.Drawing.Imaging;

namespace MvcPL.Controllers
{
    public class PhotoController : Controller
    {
        #region Fields
        private readonly ICategoryPhotoService categoryService;
        private readonly IVotingService votingService;
        private readonly IPhotoService photoService;
        private readonly IUserService userService;
        #endregion

        #region Ctors
        public PhotoController(ICategoryPhotoService service, IPhotoService phService, IUserService usrService, IVotingService voteService)
        {
            categoryService = service;
            photoService = phService;
            userService = usrService;
            votingService = voteService;
        }
        #endregion

        #region Public Methods

        public ActionResult Index(int page = 1, int Id = 0)
        {
            int pageSize = 6;
            dynamic photosPerPages;
            if (Id > 0)
                photosPerPages = photoService.GetAllPhotoEntities().Where(ph => ph.CategoryId == Id).Select(photo => photo.ToMvcPhoto()).Skip((page - 1) * pageSize).Take(pageSize);
            else
                photosPerPages = photoService.GetAllPhotoEntities().Select(photo => photo.ToMvcPhoto()).Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = photoService.GetAllPhotoEntities().Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                Photos = photosPerPages
            };
            if (HttpContext.Request.IsAjaxRequest())
                return PartialView(ivm);
            return View(ivm);
        }

        [Authorize]
        public ActionResult GetPhotoForUser(int page = 0)
        {
            int pageSize = 3;
            int userId = userService.GetUserByEmail(User.Identity.Name).UserId;
            var photos = new PagedData<PhotoViewModel>();

            var allPhotos = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .ToList();

            if (page > 0)
            {
                photos.Data = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
                photos.CurrentPage = page;
            }
            else
            {
                photos.Data = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .Take(pageSize)
                        .ToList();
                photos.CurrentPage = 1;
            }
            photos.NumberOfPages = allPhotos.Count() % pageSize == 0 ? allPhotos.Count() / pageSize : allPhotos.Count() / pageSize + 1;


            return View(photos);
        }

        public ActionResult PhotosList(int page)
        {
            int pageSize = 3;
            int userId = userService.GetUserByEmail(User.Identity.Name).UserId;
            var photos = new PagedData<PhotoViewModel>();

            var allPhotos = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .ToList();

            photos.Data = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();

            photos.NumberOfPages = allPhotos.Count() % pageSize == 0 ? allPhotos.Count() / pageSize : allPhotos.Count() / pageSize + 1;
            photos.CurrentPage = page;

            return PartialView(photos);
        }

        public ActionResult LastPhotos(string userName = null)
        {
            int userId = 0;
            if (userName == null)
                userId = userService.GetUserByEmail(User.Identity.Name).UserId;
            else
                userId = userService.GetUserByEmail(userName).UserId;
            var allPhotos = photoService.GetAllPhotoEntitiesForUser(userId).Select(photo => photo.ToMvcPhoto())
                        .ToList();

            var lastphotos = allPhotos.Skip(Math.Max(0, allPhotos.Count() - 6));

            return PartialView(lastphotos);
        }

        /// <summary>
        /// Method Photos dynamic load photos to main page.
        /// </summary>
        /// <param name="photoId">Id of last photos in main page.</param>
        /// <returns>Next 3 photos to main page.</returns>
        [HttpPost]
        public ActionResult Photos(int photoId, int Id = 0)
        {
            var photos = default(IEnumerable<PhotoViewModel>);
            if(Id > 0)
               photos = photoService.GetAllPhotoEntities().Select(photo => photo.ToMvcPhoto()).
                    Where(p => p.Id > photoId && p.CategoryId == Id).Take(3);
            else
               photos = photoService.GetAllPhotoEntities().Select(photo => photo.ToMvcPhoto()).
                    Where(p => p.Id > photoId).Take(3);
            if (photos.IsEmpty<PhotoViewModel>())
                return null;
            return PartialView("_Photos", photos);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePhoto()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePhoto(PhotoViewModel photo, IEnumerable<HttpPostedFileBase> files, string Id)
        {
            if (!ModelState.IsValid)
                return View(photo);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            PhotoViewModel model = new PhotoViewModel();
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    model.Image = Images.GetImageNewSize(file);
                }

                model.Description = photo.Description;
                model.CreatedOn = DateTime.Now;
                model.CategoryId = Convert.ToInt32(Id);
                model.UserId = userService.GetUserByEmail(User.Identity.Name).UserId;
                photoService.CreatePhoto(model.ToBllPhoto());
            }

            return RedirectToAction("GetPhotoForUser");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditPhoto(int photoId)
        {
            var photo = photoService.GetPhotoEntity(photoId).ToMvcPhoto();
            return View(photo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditPhoto(PhotoViewModel photo, int photoId, int id)
        {
            if (ModelState.IsValid)
            {
                photo.Id = photoId;
                photo.CategoryId = id;
                photoService.UpdatePhoto(photo.ToBllPhoto());
                TempData["message"] = string.Format("Photo {0}  has been updated.", photo.Description);
                return RedirectToAction("GetPhotoForUser");
            }

            return View(photo);
        }

        [HttpGet]
        public ActionResult DeletePhoto(int id)
        {
            PhotoEntity photo = photoService.GetPhotoEntity(id);

            if (photo == null)
                return HttpNotFound();

            return View(photo.ToMvcPhoto());
        }

        [HttpPost, ActionName("DeletePhoto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotoEntity photo = photoService.GetPhotoEntity(id);
            TempData["message"] = string.Format("Photo has been deleted.", photo.Description);
            photoService.DeletePhoto(photo);
            return RedirectToAction("GetPhotoForUser");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PhotoEntity photo = photoService.GetPhotoEntity(id);
            TempData["message"] = string.Format("Photo has been deleted.", photo.Description);
            photoService.DeletePhoto(photo);            
            return RedirectToAction("GetPhotoForUser");
        }

        public ActionResult FindByDescription(string term)
        {

            var photos = photoService.GetAllPhotoEntities().Where(photo => photo.Description.ToLower().Contains(term.ToLower()));
            var results = photos.Select(photo =>
                new
                {
                    id = photo.Id,
                    label = photo.Description
                });

            var jsonResult = Json(results.ToList(), JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        [HttpGet]
        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(string term)
        {
            IEnumerable<PhotoViewModel> photos = photoService.GetAllPhotoEntities().Where(photo => photo.Description.ToLower().Contains(term.ToLower()) || photo.Description.ToLower() == term.ToLower()).
                Select(photo => new PhotoViewModel { CreatedOn = photo.CreatedOn, CategoryId = photo.CategoryId, Id = photo.Id });

            return View(photos);
        }

        public JsonResult PhotoListBySearch(string search)
        {
            var photos = photoService.GetAllPhotoEntities().Where(photo => photo.Description.ToLower().Contains(search.ToLower())).
                Select(photo => new { photo.CreatedOn, photo.CategoryId, photo.Id });

            return Json(photos, JsonRequestBehavior.AllowGet);
        }

        [AcceptAjax]
        public ActionResult Details(int photoId)
        {
            var photo = photoService.GetPhotoEntity(photoId);
            return Json(photo, JsonRequestBehavior.AllowGet);
        }

        [ActionName("Details")]
        public ActionResult Details_NonAjax(int photoId)
        {
            var photo = photoService.GetPhotoEntity(photoId);
            return View("Details", photo.ToMvcPhoto());
        }

        public Image GetUserImage(string userName)
        {
            var user = userService.GetUserByEmail(userName);

            if ((user.Photo) != null)
                return GetImageFromBytes(user.Photo);
            else
                return null;
        }

        public Image GetImage(int photoId)
        {
            var photo = photoService.GetPhotoEntity(photoId);

            if ((photo.Image) != null)
                return GetImageFromBytes(photo.Image);
            else
                return null;
        }

        public Image GetImageFromBytes(byte[] photo)
        {
            using (var ms = new MemoryStream(photo))
            {
                Image img = Image.FromStream(ms);
                img.Save(Response.OutputStream, ImageFormat.Png);

                return img;
            }
        }

        #endregion
    }
}