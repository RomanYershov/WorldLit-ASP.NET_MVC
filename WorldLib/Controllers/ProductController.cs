﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldLib.Models;
using WorldLib.Services;

namespace WorldLib.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var model = ViewProductModel.Load();
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProduct(SaveProductModel product)
        {
            product.Save();
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            var rep = new Repository<Product>();
            rep.Delete(x => x.Id == id);
            rep.Commit();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}