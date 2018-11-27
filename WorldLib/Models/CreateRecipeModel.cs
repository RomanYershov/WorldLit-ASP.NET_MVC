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
        public string Image { get; set; }       

        public Recipe RecipeBuild()
        {
            return new Recipe
            {
                Name = NewRecipeName,
                FoodCategoryId = CategoryId,
                Description = TextBuild(),
                ImageUrl = $"/Content/Images/{Image}"
            };
        }

        private string TextBuild()
        {
            var stringBuilder = new StringBuilder();
            if (DescriptionSteps?.Count > 0)
            {
                for (int i = 0; i < DescriptionSteps.Count; i++)
                {
                    stringBuilder.Append($"Шаг {i + 1} {Environment.NewLine}{DescriptionSteps[i]}{Environment.NewLine}");
                }
            }
            return stringBuilder.ToString();
        }
    }
}