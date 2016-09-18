using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // static routes
            routes.MapRoute("home", "", new { controller = "Photo", action = "Index" });
            routes.MapRoute("contact", "contact", new { controller = "Home", action = "Contact" });
            routes.MapRoute("about", "about", new { controller = "Home", action = "About" });
            routes.MapRoute("register", "registry",
                new { controller = "Account", action = "Register" });
            routes.MapRoute("login", "login",
                new { controller = "Account", action = "LogOn" });
            routes.MapRoute("logoff", "logoff",
                new { controller = "Account", action = "LogOff" });
            routes.MapRoute("captcha", "captcha",
                new { controller = "Account", action = "Captcha" });
            routes.MapRoute("userimg", "userimage",
               new { controller = "Home", action = "GetPhoto" });

            routes.MapRoute("addvoting", "addvote",
                new { controller = "Voting", action = "AddVoting" });
            // routes for users
            routes.MapRoute("users", "users",
                new { controller = "Admin", action = "Index" });
            routes.MapRoute("usersdetails", "users/details",
                new { controller = "Admin", action = "Details" });

            routes.MapRoute("userphotos", "myphotos",
                new { controller = "Photo", action = "GetPhotoForUser" });

            routes.MapRoute("validateuser", "userexist",
                defaults: new { controller = "Account", action = "ValidateUserEmail" });

            // routes for photo
            routes.MapRoute("photosearch", "search/{term}",
            new { controller = "Photo", action = "Find", term = UrlParameter.Optional });
            routes.MapRoute("photodetails", "photo/details/{photoId}",
                 new { controller = "Photo", action = "Details", photoId = UrlParameter.Optional });
            routes.MapRoute("photoimg", "images/{photoId}",
            new { controller = "Photo", action = "GetImage", photoId = UrlParameter.Optional });

            routes.MapRoute("loaduserphotos", "loadmyphotos/{page}",
                new { controller = "Photo", action = "PhotosList", page = UrlParameter.Optional });
            routes.MapRoute("allphotos", "photos/{id}",
                new { controller = "Photo", action = "Photos", id = UrlParameter.Optional });
            routes.MapRoute("autocomplete", "autocomplete/{term}",
                new { controller = "Photo", action = "FindByDescription", term = UrlParameter.Optional });
            routes.MapRoute("photodelete", "photo/delete/{id}",
                new { controller = "Photo", action = "DeletePhoto", id = UrlParameter.Optional });
            routes.MapRoute("photodel", "myphotos/delete/{id}",
                new { controller = "Photo", action = "Delete", id = UrlParameter.Optional });
            routes.MapRoute("photocreate", "photo/create",
                new { controller = "Photo", action = "CreatePhoto" });
            routes.MapRoute("photoedit", "photo/{photoId}/{action}",
                new { controller = "Photo", action = "EditPhoto" });
            routes.MapRoute("lastphoto", "lastphoto",
                new { controller = "Photo", action = "LastPhotos" });

            // routes for category
            routes.MapRoute("categorycreate", "category/create",
                new { controller = "Category", action = "CreateCategory" });
            routes.MapRoute("category", "category",
                new { controller = "Category", action = "GetCategory" });
            routes.MapRoute("validate", "{action}",
                defaults: new { controller = "Category", action = "ValidateCategoryName" });

            //routes for rating
            /*routes.MapRoute("addvoting", "addvote",
                new { controller = "Voting", action = "AddVoting" });*/
            routes.MapRoute("photorating", "{action}",
                defaults: new { controller = "Voting", action = "GetTotalRating" });
            routes.MapRoute("getroles", "{action}",
                defaults: new { controller = "Account", action = "GetRolesForUser" });
            routes.MapRoute("deleteuser", "users/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteUser", id = UrlParameter.Optional });
            routes.MapRoute("delete", "users/delete/{id}",
                defaults: new { controller = "Admin", action = "Delete", id = UrlParameter.Optional });

            routes.MapRoute("404-catch-all", "{*catchall}", new { Controller = "Error", Action = "NotFound" });
        }
    }
}
