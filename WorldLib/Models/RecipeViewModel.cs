using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class RecipeViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }   
        public static List<RecipeViewModel> Load()
        {
            var model = new List<RecipeViewModel>();
            var rep = new Repository<FoodCategory>();
            var categories = rep.GetWithInclude(x => x.Id != 0, x => x.Recipes);
            foreach (var foodCategory in categories)    
            {
               
                model.Add(new RecipeViewModel
                {
                    Id = foodCategory.Id,
                    Name = foodCategory.Name,
                    Recipes = new List<Recipe>(foodCategory.Recipes)
                });
            }
            return model;
        }
    }
}