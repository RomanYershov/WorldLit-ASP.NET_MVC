using System.Linq;
using System.Web.Mvc;
using WorldLib.Helpers;
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

        [HttpPost]
        public ActionResult AddComment(CommentCreateModel model)//todo 
        {
            if (Request.IsAjaxRequest())
            {
                using (var commentRep = new Repository<Comment>())
                {
                    Comment comment = model.Create();
                    commentRep.Create(comment);
                    commentRep.Commit();
                }
            }
            return Json("Комментарий будет добавлен после успешной модерации");
        }

        [HttpGet]
        public ActionResult AddDiscussion(int id)
        {
            Category category;
            using (var categoryRep = new Repository<Category>())
            {
                category = categoryRep.Get(x => x.Id == id).SingleOrDefault();
            }
            return PartialView(category);
        }

        [HttpPost]
        public ActionResult AddDiscussion(DiscussionCreateModel model)
        {
            using (var discussionRep = new Repository<Discussion>())
            {
                Discussion discussion = model.Create();
                discussionRep.Create(discussion);
                discussionRep.Commit();
            }
            return RedirectToAction("Index");
        }
    }
}