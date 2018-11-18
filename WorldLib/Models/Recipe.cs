using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int FoodCategoryId { get; set; }
       
        public string Description { get; set; }
        public ISet<RecipeComment> RecipeComments { get; set; }

        public Recipe()
        {
            RecipeComments = new HashSet<RecipeComment>();
        }
    }

    public class FoodCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ISet<Recipe> Recipes { get; set; }

        public FoodCategory()
        {
            Recipes = new HashSet<Recipe>();
        }
    }
        
    public class RecipeComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}