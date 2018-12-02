using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldLib.Enums;
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
        public ActionResult GetCategories()     
        {
            var model = RecipeViewModel.Load();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateComment(int recipeId, string text)
        {
            var rep = new Repository<RecipeComment>();
            var comment = new RecipeComment
            {
                RecipeId = recipeId,
                Text = text,
                CreateDateTime = DateTime.Now,
                Status = CommentStatusEnum.Moderation,
                UserId =  HttpContext.User.Identity.GetUserId()
            };
            rep.Create(comment);
            rep.Commit();
            return Json("Комментарий будет добавлен после успешной модерации", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCommentsByRecipeId(int recipeId)
        {
            var model = RecipeCommentViewModel.Load(recipeId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}