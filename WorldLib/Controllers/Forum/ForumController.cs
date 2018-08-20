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

            var commentRep = new Repository<Comment>();
            List<Comment> commentsFromDiscussion =
                   commentRep.GetWithInclude(c => c.Discussion.Id == id, d => d.Discussion, u => u.User).ToList();
            return View(commentsFromDiscussion);
        }

        public ActionResult AddComment(CommentCreateModel model)
        {
            var commentRep = new Repository<Comment>();
            var userRep = new Repository<ApplicationUser>();
            var discussionRep = new Repository<Discussion>();

            Comment comment = new Comment
            {
                CreationDateTime = DateTime.Now,
                User = userRep.Get(x => x.UserName == User.Identity.Name).FirstOrDefault(),
                Discussion = discussionRep.Get(x => x.Id == model.DiscussionId).FirstOrDefault(),
                Text = model.Text
            };
            //commentRep.Create(comment);
            commentRep.Commit();
            return RedirectToAction("Comments", new { id = model.DiscussionId });
        }
    }
}