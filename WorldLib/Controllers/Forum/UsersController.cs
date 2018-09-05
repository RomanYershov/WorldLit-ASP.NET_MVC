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
    public class UsersController : Controller
    {
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
        public ActionResult Create(ApplicationUser model)
        {
            return View();
        }
    }
}