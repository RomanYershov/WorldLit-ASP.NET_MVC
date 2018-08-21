using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers.Forum
{
    public class ForumController : Controller
    {
     
        public ActionResult Index()
        {
            var model = ForumViewModel.GetModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Comments(int id)
        {
            ViewBag.CategoriesList = new Repository<Category>()
                .Get();

            var model = new CommentsByDiscussionViewModel();
            model.CreateModel(id);
            return View(model);
        }

        
        public ActionResult AddComment(CommentCreateModel model)//todo 
        {
            var commentRep = new Repository<Comment>();
            
            var discussionRep = new Repository<Discussion>();

            Comment comment = new Comment
            {
                CreationDateTime = DateTime.Now,
                UserName = User.Identity.Name,
                DiscussionId = model.DiscussionId,
                Text = model.Text
            };
            commentRep.Create(comment);
            commentRep.Commit();
            return RedirectToAction("Comments", new { id = model.DiscussionId });
        }
    }
}