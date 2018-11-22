
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using WorldLib.Enums;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class SaveProductModel
    {
        public int Id { get; set; }
        public float Cost { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }   
        public bool IsNewOrUpdatedProduct { get; set; }
        public List<Ingridient> Ingridients { get; set; }


        public void Save()
        {
            var repIngr = new Repository<Ingridient>();
            var repProd = new Repository<Product>();

            if (this.Id == 0)
            {
                SaveNewProduct(repProd);
                return;
            }
            if (IsNewOrUpdatedProduct)
                UpdateProduct(repProd);

            var forCreateIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Create).ToList();
            var forUpdateIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Update).ToList();
            var forRemoveIngr = Ingridients.Where(x => x.ProcessFlag == CrudFlagEnum.Delete && x.Id != 0).ToList();


            if (forCreateIngr.Any())
                AddIngridient(repIngr, forCreateIngr);
            if (forUpdateIngr.Any())
                UpdateIngridients(repIngr, forUpdateIngr);
            if (forRemoveIngr.Any())
                RemoveIngridient(repIngr, forRemoveIngr);

        }

        private void SaveNewProduct(Repository<Product> repProd)
        {
            Product product = new Product
            {
                Name = this.Name,
                Cost = this.Cost,
                Description = this.Description,
                Weight = this.Weight,
                Total = this.Total,
                UserId = HttpContext.Current.User.Identity.GetUserId()
            };
            repProd.Create(product);
            repProd.Commit();
            this.Id = product.Id;
            foreach (var ingridient in Ingridients)
            {
                if (ingridient.ProcessFlag == CrudFlagEnum.Delete) continue;
                ingridient.ProductId = product.Id;
            }
            product.Ingridients = new HashSet<Ingridient>(Ingridients);
            repProd.Update(product);
            repProd.Commit();
        }

        private void UpdateIngridients(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var ingridient in ingridients)
            {
                ingridient.ProcessFlag = CrudFlagEnum.None;
                ingrRep.Update(ingridient);
            }
            ingrRep.Commit();
        }

        private void RemoveIngridient(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var item in ingridients)
            {
                ingrRep.Delete(item);
                Ingridients.Remove(item);
            }
            ingrRep.Commit();
        }

        private void AddIngridient(Repository<Ingridient> ingrRep, IEnumerable<Ingridient> ingridients)
        {
            foreach (var item in ingridients)
            {
                item.ProcessFlag = CrudFlagEnum.None;
                item.ProductId = this.Id;
                ingrRep.Create(item);
            }
            ingrRep.Commit();
        }

        private void UpdateProduct(Repository<Product> repProduct)
        {
            var product = repProduct.Get(x => x.Id == Id).SingleOrDefault();
            if (product != null)
            {
                product.Name = Name;
                product.Cost = Cost;
                product.Weight = Weight;
                product.Description = Description;
                product.Total = Total;
                repProduct.Update(product);
                repProduct.Commit();
            }
        }

    }

}