using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Enums;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class RecipeCommentViewModel
    {
        public DateTime CreateDateTime { get; set; }    
        public string Text { get; set; }
        public CommentStatusEnum Status { get; set; }
        public string Author { get; set; }


        public static IEnumerable<RecipeCommentViewModel> Load(int recipeId)
        {
            var model = new List<RecipeCommentViewModel>();
            var rep = new Repository<RecipeComment>();
            var comments = rep.GetWithInclude(x => x.RecipeId == recipeId && x.Status == CommentStatusEnum.Published, x => x.User);
            foreach (var comment in comments)
            {
                    model.Add(new RecipeCommentViewModel
                    {
                        Status = comment.Status,
                        Text = comment.Text,
                        Author = comment.User.NikName,
                        CreateDateTime = comment.CreateDateTime
                    });
            }

            return model;
        }
    }
}