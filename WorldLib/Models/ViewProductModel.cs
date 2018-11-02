using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public  class ViewProductModel
    {
        public Product Product { get; set; }
        public List<Ingridient> Ingridients { get; set; }

        public static List<ViewProductModel> Load()
        {
            List<ViewProductModel> productModels = new List<ViewProductModel>();
            var prodRep = new Repository<Product>();
            var ingrRep = new Repository<Ingridient>();
            var products = prodRep.Get();
            foreach (var product in products)
            {
                var ingridients = ingrRep.Get(x => x.ProductId == product.Id).ToList();
                productModels.Add(new ViewProductModel{Product = product, Ingridients = ingridients});
            }
            return productModels;
        }
    }
}