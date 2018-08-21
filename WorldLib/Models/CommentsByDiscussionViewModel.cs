using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldLib.Services;

namespace WorldLib.Models
{
    public  class CommentsByDiscussionViewModel
    {
        public  List<Comment> Comments { get; private set; }
        public  Discussion Discussion { get; private set; }

        public  void CreateModel(int discussionId)
        {
            var discRep = new Repository<Discussion>();
            
                var commentRep = new Repository<Comment>();

                Comments = commentRep.Get(x => x.DiscussionId == discussionId).ToList();
                Discussion = discRep.GetWithInclude(x => x.Id == discussionId, c => c.Category).FirstOrDefault();
            
        
        }
    }
}