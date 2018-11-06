using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using WorldLib.Enums;
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


        public  void  Save()
        {
            var repIngr = new Repository<Ingridient>();
            var repProd = new Repository<Product>();

            if (this.Id == 0)
            {
                SaveNewProduct(repProd, repIngr);
                return;
            }

            var forCreateIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Create).ToList();
            var forUpdateIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Update).ToList();
            var forRemoveIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Delete && x.Id != 0).ToList();

           
            if(forCreateIngr.Any())
                AddIngridient(repIngr, forCreateIngr);
            if(forUpdateIngr.Any())
                UpdateIngridients(repIngr,forUpdateIngr);
            if(forRemoveIngr.Any())
                RemoveIngridient(repIngr,forRemoveIngr);

        }

        private void SaveNewProduct(Repository<Product> repProd, Repository<Ingridient> repIngr)
        {
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

        private void UpdateIngridients(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var item in ingridients)
            {
                item.ProcessFlag = CrudFlagEnum.None;
                ingrRep.Update(item);
            }
            ingrRep.Commit();
        }

        private void RemoveIngridient(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var item in ingridients)
            {
                ingrRep.Delete(item);
            }
            ingrRep.Commit();
        }

        private void AddIngridient(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var item in ingridients)
            {
                item.ProcessFlag = CrudFlagEnum.None;
                ingrRep.Create(item);
            }
            ingrRep.Commit();
        }

    }   

}