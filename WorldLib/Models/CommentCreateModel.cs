using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public class CommentCreateModel
    {
        public string Text { get; set; }
        public int DiscussionId { get; set; }
    }
}