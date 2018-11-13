using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRecipes()
        {
            var model = RecipeViewModel.Load();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}