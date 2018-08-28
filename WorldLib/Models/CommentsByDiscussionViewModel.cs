using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Enums;
using WorldLib.Services;

namespace WorldLib.Models
{
    public class CommentsByDiscussionViewModel
    {
        public List<Comment> Comments { get; private set; }
        public Discussion Discussion { get; private set; }

        public void CreateModel(int discussionId)
        {
            using (var discRep = new Repository<Discussion>())
            {
                var commentRep = new Repository<Comment>();
                Comments = commentRep.Get(x => x.DiscussionId == discussionId && x.Status == CommentStatusEnum.Published).ToList();
                Discussion = discRep.Get(x => x.Id == discussionId).SingleOrDefault();
            }
        }
    }
}