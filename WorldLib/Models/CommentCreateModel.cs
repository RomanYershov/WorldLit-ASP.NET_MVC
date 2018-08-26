using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class CommentCreateModel : IModelBuilder<Comment>
    {
        public string Text { get; set; }
        public int DiscussionId { get; set; }

        public Comment Create()
        {
            return new Comment
            {
                CreationDateTime = DateTime.Now,
                UserId = HttpContext.Current.User.Identity.GetUserId(),                                //userManager.FindByEmail(User.Identity.Name),
                DiscussionId = DiscussionId,
                Text = Text
            };
        }
    }
}