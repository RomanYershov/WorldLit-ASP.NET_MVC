using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WorldLib.Models
{
    public class CreateRecipeModel
    {
        public string NewRecipeName { get; set; }   
        public List<string> DescriptionSteps { get; set; }
        public int CategoryId { get; set; }

        public Recipe RecipeBuild()
        {

            return new Recipe
            {
                Name = NewRecipeName,
                FoodCategoryId = CategoryId,
                Description = TextBuild()
            };
        }

        private string TextBuild()
        {
            var stringBuilder = new StringBuilder();
            if (DescriptionSteps.Count > 0)
            {
                foreach (var step in DescriptionSteps)
                {
                    stringBuilder.Append(" - " + step + "\n\n");
                }
            }
            return stringBuilder.ToString();
        }
    }
}