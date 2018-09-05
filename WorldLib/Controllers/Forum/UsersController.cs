using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers.Forum
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        // GET: Users
        public ActionResult Index()
        {
            var rep = new Repository<ApplicationUser>();
            var users = rep.Get();
            return View(users);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            var user = new ApplicationUser
            {
                NikName = model.Name,
                UserName = model.Email,
                Email = model.Email
            };
            var result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, model.Role);
            }
            return Json("Success");
        }
    }
}