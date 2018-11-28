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
        [HttpPost]
        public ActionResult CreateRecipe(CreateRecipeModel recipe)
        {
            
            if (ModelState.IsValid)
            {
                var rep = new Repository<Recipe>();
                var repCtegory = new Repository<FoodCategory>();
                var newRecipe = recipe.RecipeBuild();
                rep.Create(newRecipe);
                rep.Commit();
                var categoryName = repCtegory.Get(x => x.Id == newRecipe.FoodCategoryId).FirstOrDefault();
                if (categoryName != null)
                    return Json(new
                    {
                        Id = newRecipe.Id,
                        Name = newRecipe.Name,
                        Description = newRecipe.Description,
                        ImageUrl = newRecipe.ImageUrl,
                        CategoryName = categoryName.Name
                    }, JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFile()
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    upload.SaveAs(Server.MapPath("~/Content/Images/"+upload.FileName));
                }
            }
            return Json("file is uploaded");
        }

        [HttpPost]
        public ActionResult RemoveRecipe(int id)
        {
            var rep = new Repository<Recipe>();
            rep.Delete(id);
            rep.Commit();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}