using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers.Forum
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var rep = new Repository<FoodCategory>();
            var foodCategories = rep.Get();
            return View(foodCategories);
        }
        [HttpPost]
        public ActionResult CreateCategory(string name)
        {
            var rep = new Repository<FoodCategory>();
            var category = new FoodCategory{Name = name};
            rep.Create(category);
            rep.Commit();
            return Json(category, JsonRequestBehavior.AllowGet);
        }


    }
}