using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class FoodCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static  IEnumerable<FoodCategoryViewModel> Load()
        {
            var rep = new Repository<FoodCategory>();
            var categories = rep.Get();
            var model = categories.Select(x => new FoodCategoryViewModel {Id = x.Id, Name = x.Name});
            return model;
        }
    }
}