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
            return View();
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
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var rep = new Repository<FoodCategory>();
            rep.Delete(id);
            rep.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditCategory(int id, string name)
        {
            var rep = new Repository<FoodCategory>();
            var category = rep.Get(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {
                category.Name = name;
                rep.Update(category);
            }
            rep.Commit();
            return Json(category, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Recipes()
        {
            return View();
        }

    }
}