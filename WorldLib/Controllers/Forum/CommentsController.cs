using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WorldLib.Enums;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers.Forum
{
    [Authorize(Roles = "admin")]
    public class CommentsController : Controller
    {
        // GET: Commens
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                List<Comment> commentsAjax;
                var repCommentAjax = new Repository<Comment>();
                commentsAjax = repCommentAjax.GetWithInclude(x => x.Status == CommentStatusEnum.Moderation, d => d.Discussion, u => u.User)
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreationDateTime).ToList();

                return PartialView("SelectedStatus", commentsAjax);
            }
            List<Comment> comments;
            var repComment = new Repository<Comment>();
            comments = repComment.GetWithInclude(x => x.Status == CommentStatusEnum.Moderation, d => d.Discussion, u => u.User)
                .OrderBy(x => x.Status).ThenByDescending(x => x.CreationDateTime).ToList();
            return View(comments);
        }

        public ActionResult PublishedComment(int id) //todo 
        {
            if (Request.IsAjaxRequest())
            {
                using (var rep = new Repository<Comment>())
                {
                    var comment = rep.Get(x => x.Id == id).SingleOrDefault();
                    if (comment != null)
                    {
                        comment.Status = CommentStatusEnum.Published;
                        rep.Update(comment);
                    }
                    rep.Commit();
                    return Json($"Комментарий опубликован. ID: {comment.Id}", JsonRequestBehavior.AllowGet);
                }
                //  return RedirectToAction("Index");
               
            }
            using (var rep = new Repository<Comment>())
            {
                var comment = rep.Get(x => x.Id == id).SingleOrDefault();
                if (comment != null)
                {
                    comment.Status = CommentStatusEnum.Published;
                    rep.Update(comment);
                }
                rep.Commit();
            }
            return RedirectToAction("Index");
        }

        public ActionResult SelectedStatus(CommentStatusEnum status)
        {
            IEnumerable<Comment> comments;
            var repComment = new Repository<Comment>();
            comments = status == CommentStatusEnum.All
                ? repComment.GetWithInclude(x => true, d => d.Discussion, u => u.User)
                : repComment.GetWithInclude(x => x.Status == status, d => d.Discussion, u => u.User);
            return PartialView(comments.OrderBy(x => x.Status).ThenByDescending(x => x.CreationDateTime).ToList());
        }

        [AllowAnonymous]
        public ActionResult Like(int commentId)
        {
            var repLike = new Repository<Like>();
            if (repLike.Get(x => x.CommentId == commentId && x.UserId == User.Identity.GetUserId()).Any())
            {
                return Json("exists", JsonRequestBehavior.AllowGet);
            }
            var like = new Like
            {
                UserId = User.Identity.GetUserId(),
                CommentId = commentId,
                IsLike = true
            };
            repLike.Create(like);
            repLike.Commit();
            return Json(like, JsonRequestBehavior.AllowGet);
        }

        // GET: Commens/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Commens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commens/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Commens/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment;
            using (var rep = new Repository<Comment>())
            {
                comment = rep.Get(x => x.Id == id).SingleOrDefault();
            }
            return PartialView(comment);
        }

        // POST: Commens/Edit/5
        [HttpPost]
        public ActionResult Edit( Comment model)
        {
            try
            {
               using(var rep = new Repository<Comment>())
                {
                    rep.Update(model);
                    rep.Commit();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Commens/Delete/5
        public  ActionResult Delete(int id, string email)
        {
            var commentRep = new Repository<Comment>();
            commentRep.Delete(x => x.Id == id);
            commentRep.Commit();
            var emailModel = new EmailModel
            {
                To = email,
                From = "energy26622662@gmail.com",
                Subject = email,
                Body = "Ваш отзыв не прошел модерацию и был удален администратором сайта.",
            };
            try
            {
                new EmailController().SendEmail(emailModel).Deliver();
                
            }
            catch (Exception e)
            {
                return  new HttpStatusCodeResult(404);
             }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AllComments()
        {
            var rep = new Repository<Comment>();
            var comments = rep.GetWithInclude(x => x.Status == CommentStatusEnum.Published, u => u.User).Select(x => new
            {
                CommentId = x.Id,
                Text = x.Text,
                DiscussionId = x.DiscussionId,
                Discussion = x.Discussion,
                Author = x.User.NikName,
                CreateDate = x.CreationDateTime.ToShortDateString()
            }).ToArray();


            return Json(comments, JsonRequestBehavior.AllowGet);
        }


       
    }

    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int DiscussionId { get; set; }
        public string Avtor { get; set; }   

    }
}
