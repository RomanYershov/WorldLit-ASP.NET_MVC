using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class SaveProductModel
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public List<Ingridient> Ingridients { get; set; }


        public  void Save()
        {
            var repProd = new Repository<Product>();
            var repIngr = new Repository<Ingridient>();

            Product product = new Product
            {
                Name = this.Name,
                Cost = this.Cost,
                Description = this.Description,
                Weight = this.Weight
            };
            repProd.Create(product);
            repProd.Commit();
            foreach (var ingridient in Ingridients)     
            {
                repIngr.Create(new Ingridient
                {
                    Cost = ingridient.Cost,
                    Weight = ingridient.Weight,
                    Name = ingridient.Name,
                    ProductId = product.Id
                });
            }
            repIngr.Commit();
        }
    }   

}